# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Contexto del proyecto

Sistema de gestión para los **Bomberos Voluntarios de Chivilcoy**, desarrollado como práctica
profesionalizante por estudiantes de la **E.E.S.T. N°1 "Mariano Moreno" de Chivilcoy**. Gestiona
salidas/emergencias, personal (bomberos, comisión directiva, cobradores), socios y pagos, vehículos
(flota y afectados), licencias, sanciones y equipos.

Es un proyecto de aprendizaje que evolucionó de forma dispar a lo largo de los años con distintos
estudiantes. Vas a encontrar inconsistencias: CSS repetido, lógica de negocio metida directo en
archivos `.razor` en unos lugares y en servicios en otros. El objetivo a largo plazo es ir
**homogeneizando y estandarizando** de a poco. Al tocar código, preferí mover lógica de negocio a
servicios y reutilizar componentes/estilos existentes en lugar de duplicar. Ante una decisión de
arquitectura no obvia, conviene consultar antes de propagar un patrón.

**El código y la UI están en español** (identificadores, comentarios, dominio). Escribí el código
nuevo, los comentarios y los términos de dominio en español para mantener la coherencia.

### ⚠️ La app está en producción

La aplicación ya está desplegada y en uso (funciona como un QA, pero esa **misma base de datos se va a
usar / se usa como producción**). Se puede modificar lo que sea necesario, **pero hay que cuidar los
datos existentes**: ante cualquier cambio de esquema que pueda perder o corromper datos (renombrar
columnas, cambiar tipos, cambiar la conversión de un enum int↔string, dividir/unir tablas TPH), hay que
escribir una **migración especial con SQL de transformación de datos** (custom `migrationBuilder.Sql(...)`),
no solo el cambio de esquema autogenerado. Cuando una migración toca datos productivos, conviene
revisar el SQL generado y plantearlo antes de aplicarlo.

## Comandos

Requiere el SDK de **.NET 8**. El proyecto de inicio es `FireForce.Core`.

```bash
# Compilar toda la solución
dotnet build

# Ejecutar (HTTPS https://localhost:7183  ·  HTTP http://localhost:5183)
dotnet run --project FireForce.Core

# Hot reload durante desarrollo
dotnet watch run --project FireForce.Core
```

### Migraciones de Entity Framework

El modelo de datos vive en `FireForce.Data` pero el host es `FireForce.Core`, así que **siempre** hay
que pasar ambos proyectos:

```bash
dotnet ef migrations add <Nombre> --project FireForce.Data --startup-project FireForce.Core
dotnet ef database update      --project FireForce.Data --startup-project FireForce.Core
```

No suele hacer falta `database update`: el servicio `InitData` (hosted service en `FireForce.Core/Services`)
aplica las migraciones pendientes automáticamente al arrancar (`context.Database.MigrateAsync()`).

> **Cuidado con los datos en producción** (ver arriba): como las migraciones se aplican solas al
> arrancar, una migración que pierda datos impacta directo en la base productiva. Si el cambio puede
> afectar datos existentes, agregá el SQL de transformación dentro de la migración y revisalo antes de
> mergear a `master` (el push a `master` despliega a Azure automáticamente).

No hay proyecto de tests.

### Configuración y secretos

- La cadena de conexión MySQL se lee de la clave **plana** `ConnectionString` (no `ConnectionStrings:...`).
- En desarrollo se usa **user secrets** (`FireForce.Core` tiene `UserSecretsId`):
  ```bash
  dotnet user-secrets set "ConnectionString" "Server=...;Database=bomberos;User=...;Password=...;" --project FireForce.Core
  ```
- `appsettings.json` contiene la config de Azure AD (Entra ID), Microsoft Graph y Nominatim (sin secretos).

## Arquitectura

**Blazor Server (.NET 8) + EF Core + MySQL (Pomelo)**, UI con **Ant Design Blazor**, autenticación con
**Microsoft Entra ID (Azure AD)** vía OpenID Connect + Microsoft Graph.

Cuatro proyectos. Flujo de referencias: `Shared` ← `Data` ← `Client` ← `Core`.

- **`FireForce.Core`** (`Sdk.Web`) — host ASP.NET Core y **startup project**. `Program.cs` (DI, auth,
  localización, registro de servicios), controladores, las páginas host de Blazor (`Pages/_Host.cshtml`,
  `_Layout.cshtml`) y servicios propios del host: `InitData` (aplica migraciones), Nominatim (geocoding),
  `EntraIDService`, `CircuitHandlerService`.
- **`FireForce.Client`** (`Sdk.Razor`, Razor Class Library) — la UI Blazor (`Pages`, `Components`,
  `Shared` layouts) **y la mayoría de los servicios de dominio** (`Services/BomberoService.cs`,
  `SalidaService.cs`, etc.). Ojo: aunque estos servicios están acá, se **registran** en
  `FireForce.Core/Program.cs` e inyectan `BomberosDbContext` directamente. También: `Helpers`
  (Base64, export a Excel con ClosedXML), `Data/ViewModels` y `Data/Mappers`.
- **`FireForce.Data`** — capa de datos EF Core: `BomberosDbContext`, `Models/` (dominio rico, agrupado
  por área: Personas, Salidas, Vehiculos, Socios, Grupos, Objetos, Imagenes) y `Migrations/`.
- **`FireForce.Shared`** — transversal: `Enums/` (muchísimos, con `[Display(Name=...)]`), `DTOs/`,
  `ViewModels/`, `Extensions/` (`EnumExtensions.GetDisplayName()`) e interfaces de servicio compartidas.

### Herencia TPH con discriminadores enum (lo más importante)

El modelo usa intensivamente **Table-Per-Hierarchy** con **enums como discriminador**, todo configurado
en `BomberosDbContext.OnModelCreating`. Jerarquías principales:

- `Persona` → `Bombero` / `ComisionDirectiva` / `Cobrador` (discriminador `TipoPersonal`)
- `Salida` → decenas de tipos de emergencia: `Incendio*`, `Rescate*`, `Accidente`, `MaterialPeligroso`,
  `FactorClimatico`, `ServicioEspecial*`… (discriminador `TipoDeEmergencia`)
- `Vehiculo` → `Movil` / `Embarcacion` / `VehiculoAfectado` / `Vehiculo_Personal` (discriminador `TipoVehiculo`)
- También `Seguro`, `Imagen`, `PagoSocio`.

**Al agregar un subtipo nuevo:** agregá el valor al enum discriminador y registralo con
`.HasValue<NuevoTipo>(Enum.Valor)` en `OnModelCreating`, después generá la migración.

### Convenciones de enums

- Los enums se persisten como **int** (`HasConversion<int>()`) o como **string**
  (`HasConversion<string>().HasMaxLength(255)`). Revisá el mapeo existente en `OnModelCreating` antes
  de cambiar un enum: cambiar la conversión rompe datos existentes.
- Casi todos los valores llevan `[Display(Name = "...")]` con la etiqueta en español; en la UI se leen
  con `EnumExtensions.GetDisplayName()`, no con `.ToString()`.

### Otros detalles

- DbContext registrado con `AddDbContextFactory<BomberosDbContext>`; los servicios reciben el
  `BomberosDbContext` por inyección (scoped).
- Localización fija a cultura `es-AR`; locale de Ant Design en `es-Es`.
- Autorización por roles de Entra ID mediante políticas: `AdminOnly` (rol `administrador`) y
  `SecretariaOrAdmin` (`secretaria` o `administrador`).
- `Nullable` e `ImplicitUsings` habilitados en los cuatro proyectos.

## Despliegue

GitHub Actions (`.github/workflows/master_bomberos.yml`) compila y publica `FireForce.Core` y lo
despliega en la Azure Web App **"bomberos"** ante cada push a `master`.

## Advertencias (remanentes del nombre anterior)

El proyecto se llamaba "Vista" antes. Ya se limpió casi todo (el directorio `Vista/` y las referencias
en `Dockerfile`/`docker-compose.yml`), pero queda un remanente cosmético:

- El perfil de ejecución en `FireForce.Core/Properties/launchSettings.json` todavía se llama **"Vista"**.
  Funciona igual; es solo el nombre del profile. No se renombró para no romper la memoria muscular de
  quienes lo seleccionan en Visual Studio.
