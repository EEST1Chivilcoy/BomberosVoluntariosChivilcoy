using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vista.Data.Migrations
{
    /// <inheritdoc />
    public partial class MigracionInicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Comunicacion",
                columns: table => new
                {
                    ComunicacionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NroEquipo = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Marca = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Modelo = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NroSerie = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Estado = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comunicacion", x => x.ComunicacionId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "EquiposAutonomos",
                columns: table => new
                {
                    EquipoAutonomoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NroSerie = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NroTubo = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Marca = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Modelo = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TipoMaterial = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Estado = table.Column<int>(type: "int", nullable: false),
                    UltimaFechaPruebaHidraulica = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    FechaVencimientoPruebaHidraulica = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquiposAutonomos", x => x.EquipoAutonomoId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Fuerzas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NombreFuerza = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fuerzas", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Materiales",
                columns: table => new
                {
                    MaterialId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FechaAlta = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Descripcion = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Codigo = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Stock = table.Column<int>(type: "int", nullable: false),
                    Rubro = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Materiales", x => x.MaterialId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PartesVehiculo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Tipo = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PartesVehiculo", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Socios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NroSocio = table.Column<int>(type: "int", nullable: false),
                    Tipo = table.Column<int>(type: "int", nullable: false),
                    FechaIngreso = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    FechaIngresoSistemaNuevo = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    EstadoSocio = table.Column<int>(type: "int", nullable: false),
                    MontoCuota = table.Column<double>(type: "double", nullable: false),
                    FrecuenciaDePago = table.Column<int>(type: "int", nullable: false),
                    FormaPago = table.Column<int>(type: "int", nullable: false),
                    DocumentoOCUIT = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Nombre = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Apellido = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Direccion = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Latitud = table.Column<double>(type: "double", nullable: false),
                    Longitud = table.Column<double>(type: "double", nullable: false),
                    Zona = table.Column<int>(type: "int", nullable: false),
                    Ocupacion = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Telefono = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Socios", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Persona",
                columns: table => new
                {
                    PersonaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Tipo = table.Column<int>(type: "int", nullable: false),
                    Sexo = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Nombre = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Apellido = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Documento = table.Column<int>(type: "int", nullable: false),
                    Residencia = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Direccion = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Edad = table.Column<int>(type: "int", nullable: false),
                    EntraId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    UPN = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    GrupoSanguineo = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FechaNacimiento = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    LugarNacimiento = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FechaAceptacion = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    NumeroLegajo = table.Column<int>(type: "int", nullable: true),
                    Estado = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Observaciones = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Profesion = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NivelEstudios = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NumeroIoma = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Dotacion = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Grado = table.Column<int>(type: "int", nullable: true),
                    Altura = table.Column<int>(type: "int", nullable: true),
                    Peso = table.Column<int>(type: "int", nullable: true),
                    Chofer = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    VencimientoRegistro = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    EquipoId = table.Column<int>(type: "int", nullable: true),
                    Cobrador_Estado = table.Column<int>(type: "int", nullable: true),
                    ZonasAsignadas = table.Column<int>(type: "int", nullable: true),
                    ComisionDirectiva_Grado = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ComisionDirectiva_Estado = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persona", x => x.PersonaId);
                    table.ForeignKey(
                        name: "FK_Persona_Comunicacion_EquipoId",
                        column: x => x.EquipoId,
                        principalTable: "Comunicacion",
                        principalColumn: "ComunicacionId",
                        onDelete: ReferentialAction.SetNull);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "HistorialSocios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FechaDesde = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    FechaHasta = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    SocioId = table.Column<int>(type: "int", nullable: false),
                    Discriminator = table.Column<string>(type: "varchar(34)", maxLength: 34, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FrecuenciaDePago = table.Column<int>(type: "int", nullable: true),
                    FormaDePago = table.Column<int>(type: "int", nullable: true),
                    Monto = table.Column<double>(type: "double", nullable: true),
                    Estado = table.Column<int>(type: "int", nullable: true),
                    Motivo = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistorialSocios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HistorialSocios_Socios_SocioId",
                        column: x => x.SocioId,
                        principalTable: "Socios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AscensoBomberos",
                columns: table => new
                {
                    AscensoBomberoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FechaAscenso = table.Column<DateOnly>(type: "date", nullable: false),
                    Descripcion = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NumeroLibero = table.Column<int>(type: "int", nullable: false),
                    NumeroActa = table.Column<int>(type: "int", nullable: false),
                    GradoAntiguo = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    GradoAscenso = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PersonaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AscensoBomberos", x => x.AscensoBomberoId);
                    table.ForeignKey(
                        name: "FK_AscensoBomberos_Persona_PersonaId",
                        column: x => x.PersonaId,
                        principalTable: "Persona",
                        principalColumn: "PersonaId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Brigadas",
                columns: table => new
                {
                    BrigadaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NombreBrigada = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EncargadoPersonaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brigadas", x => x.BrigadaId);
                    table.ForeignKey(
                        name: "FK_Brigadas_Persona_EncargadoPersonaId",
                        column: x => x.EncargadoPersonaId,
                        principalTable: "Persona",
                        principalColumn: "PersonaId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Contactos",
                columns: table => new
                {
                    ContactoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TelefonoCel = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TelefonoLaboral = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PersonalId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contactos", x => x.ContactoId);
                    table.ForeignKey(
                        name: "FK_Contactos_Persona_PersonalId",
                        column: x => x.PersonalId,
                        principalTable: "Persona",
                        principalColumn: "PersonaId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Dependencias",
                columns: table => new
                {
                    DependenciaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NombreDependencia = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EncargadoPersonaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dependencias", x => x.DependenciaId);
                    table.ForeignKey(
                        name: "FK_Dependencias_Persona_EncargadoPersonaId",
                        column: x => x.EncargadoPersonaId,
                        principalTable: "Persona",
                        principalColumn: "PersonaId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Licencias",
                columns: table => new
                {
                    LicenciaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TipoLicencia = table.Column<int>(type: "int", nullable: false),
                    Descripcion = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Desde = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Hasta = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    EstadoLicencia = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RazonRechazo = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PersonalId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Licencias", x => x.LicenciaId);
                    table.ForeignKey(
                        name: "FK_Licencias_Persona_PersonalId",
                        column: x => x.PersonalId,
                        principalTable: "Persona",
                        principalColumn: "PersonaId",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PagoSocio",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Tipo = table.Column<int>(type: "int", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    FechaConfirmadoORechazado = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Monto = table.Column<double>(type: "double", nullable: false),
                    SocioId = table.Column<int>(type: "int", nullable: false),
                    Estado = table.Column<int>(type: "int", nullable: false),
                    ConfirmadoPorPersonaId = table.Column<int>(type: "int", nullable: true),
                    CobradorId = table.Column<int>(type: "int", nullable: true),
                    FechaEntregaAComision = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Observaciones = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PagoSocio", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PagoSocio_Persona_CobradorId",
                        column: x => x.CobradorId,
                        principalTable: "Persona",
                        principalColumn: "PersonaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PagoSocio_Persona_ConfirmadoPorPersonaId",
                        column: x => x.ConfirmadoPorPersonaId,
                        principalTable: "Persona",
                        principalColumn: "PersonaId");
                    table.ForeignKey(
                        name: "FK_PagoSocio_Socios_SocioId",
                        column: x => x.SocioId,
                        principalTable: "Socios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Sanciones",
                columns: table => new
                {
                    SancionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FechaDesde = table.Column<DateOnly>(type: "date", nullable: false),
                    FechaHasta = table.Column<DateOnly>(type: "date", nullable: false),
                    PersonaId = table.Column<int>(type: "int", nullable: false),
                    TipoSancion = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SacionArea = table.Column<int>(type: "int", nullable: false),
                    DescripcionSancion = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sanciones", x => x.SancionId);
                    table.ForeignKey(
                        name: "FK_Sanciones_Persona_PersonaId",
                        column: x => x.PersonaId,
                        principalTable: "Persona",
                        principalColumn: "PersonaId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "bombero_brigada",
                columns: table => new
                {
                    PersonaId = table.Column<int>(type: "int", nullable: false),
                    BrigadaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bombero_brigada", x => new { x.PersonaId, x.BrigadaId });
                    table.ForeignKey(
                        name: "FK_bombero_brigada_Brigadas_BrigadaId",
                        column: x => x.BrigadaId,
                        principalTable: "Brigadas",
                        principalColumn: "BrigadaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_bombero_brigada_Persona_PersonaId",
                        column: x => x.PersonaId,
                        principalTable: "Persona",
                        principalColumn: "PersonaId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "bombero_dependencia",
                columns: table => new
                {
                    PersonaId = table.Column<int>(type: "int", nullable: false),
                    DependenciaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bombero_dependencia", x => new { x.PersonaId, x.DependenciaId });
                    table.ForeignKey(
                        name: "FK_bombero_dependencia_Dependencias_DependenciaId",
                        column: x => x.DependenciaId,
                        principalTable: "Dependencias",
                        principalColumn: "DependenciaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_bombero_dependencia_Persona_PersonaId",
                        column: x => x.PersonaId,
                        principalTable: "Persona",
                        principalColumn: "PersonaId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Imagen",
                columns: table => new
                {
                    ImagenId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Tipo = table.Column<int>(type: "int", nullable: false),
                    NombreImagen = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DatosImagen = table.Column<byte[]>(type: "longblob", nullable: false),
                    TipoImagen = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LicenciaId = table.Column<int>(type: "int", nullable: true),
                    PersonalId = table.Column<int>(type: "int", nullable: true),
                    VehiculoId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Imagen", x => x.ImagenId);
                    table.ForeignKey(
                        name: "FK_Imagen_Licencias_LicenciaId",
                        column: x => x.LicenciaId,
                        principalTable: "Licencias",
                        principalColumn: "LicenciaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Imagen_Persona_PersonalId",
                        column: x => x.PersonalId,
                        principalTable: "Persona",
                        principalColumn: "PersonaId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "BomberosSalida",
                columns: table => new
                {
                    BomberoSalidaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    SalidaId = table.Column<int>(type: "int", nullable: false),
                    MovilId = table.Column<int>(type: "int", nullable: true),
                    Grado = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PersonaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BomberosSalida", x => x.BomberoSalidaId);
                    table.ForeignKey(
                        name: "FK_BomberosSalida_Persona_PersonaId",
                        column: x => x.PersonaId,
                        principalTable: "Persona",
                        principalColumn: "PersonaId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Damnificados",
                columns: table => new
                {
                    Damnificado_SalidaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Apellido = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Sexo = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LugarNacimiento = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Documento = table.Column<int>(type: "int", nullable: true),
                    Edad = table.Column<int>(type: "int", nullable: true),
                    Estado = table.Column<int>(type: "int", nullable: true),
                    FuerzaIntervinienteId = table.Column<int>(type: "int", nullable: true),
                    Destino = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SalidaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Damnificados", x => x.Damnificado_SalidaId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "EmbarcacionesAfectadas",
                columns: table => new
                {
                    EmbarcacionAfectadaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Intervinientes = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CantidadBarcos = table.Column<int>(type: "int", nullable: false),
                    CantidadBotes = table.Column<int>(type: "int", nullable: false),
                    CantidadFragatas = table.Column<int>(type: "int", nullable: false),
                    CantidadLanchas = table.Column<int>(type: "int", nullable: false),
                    Otro = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SalidaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmbarcacionesAfectadas", x => x.EmbarcacionAfectadaId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "fuerzaInterviniente_Salidas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NumeroUnidad = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EncargadoApellidoyNombre = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FuerzaIntervinienteId = table.Column<int>(type: "int", nullable: false),
                    SalidaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_fuerzaInterviniente_Salidas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_fuerzaInterviniente_Salidas_Fuerzas_FuerzaIntervinienteId",
                        column: x => x.FuerzaIntervinienteId,
                        principalTable: "Fuerzas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "MovilesSalida",
                columns: table => new
                {
                    Movil_SalidaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CargoCombustible = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    NumeroFactura = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FechaFactura = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    TipoConbustible = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CantidadLitros = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    QuienLleno = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TelefonoQuienLleno = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DotacionSalida = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SalidaId = table.Column<int>(type: "int", nullable: false),
                    PersonaId = table.Column<int>(type: "int", nullable: false),
                    MovilId = table.Column<int>(type: "int", nullable: false),
                    KmLlegada = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovilesSalida", x => x.Movil_SalidaId);
                    table.ForeignKey(
                        name: "FK_MovilesSalida_Persona_PersonaId",
                        column: x => x.PersonaId,
                        principalTable: "Persona",
                        principalColumn: "PersonaId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Movimientos",
                columns: table => new
                {
                    MovimientoMaterialId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FechaIngreso = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Observaciones = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TipoMovimiento = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DestinoBomberoPersonaId = table.Column<int>(type: "int", nullable: true),
                    DestinoMovilVehiculoId = table.Column<int>(type: "int", nullable: true),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    MaterialesMaterialId = table.Column<int>(type: "int", nullable: true),
                    esComisionDirectiva = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movimientos", x => x.MovimientoMaterialId);
                    table.ForeignKey(
                        name: "FK_Movimientos_Materiales_MaterialesMaterialId",
                        column: x => x.MaterialesMaterialId,
                        principalTable: "Materiales",
                        principalColumn: "MaterialId");
                    table.ForeignKey(
                        name: "FK_Movimientos_Persona_DestinoBomberoPersonaId",
                        column: x => x.DestinoBomberoPersonaId,
                        principalTable: "Persona",
                        principalColumn: "PersonaId");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "MovimientosEquiposAutonomos",
                columns: table => new
                {
                    Movimiento_EquipoAutonomoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    EquipoAutonomoId = table.Column<int>(type: "int", nullable: false),
                    FechaMovimiento = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    EncargadoId = table.Column<int>(type: "int", nullable: false),
                    EstadoAnterior = table.Column<int>(type: "int", nullable: false),
                    EstadoNuevo = table.Column<int>(type: "int", nullable: false),
                    AgenteAnterior = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    VehiculoDestinoId = table.Column<int>(type: "int", nullable: true),
                    DependenciaDestinoId = table.Column<int>(type: "int", nullable: true),
                    OtroDestino = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovimientosEquiposAutonomos", x => x.Movimiento_EquipoAutonomoId);
                    table.ForeignKey(
                        name: "FK_MovimientosEquiposAutonomos_Dependencias_DependenciaDestinoId",
                        column: x => x.DependenciaDestinoId,
                        principalTable: "Dependencias",
                        principalColumn: "DependenciaId");
                    table.ForeignKey(
                        name: "FK_MovimientosEquiposAutonomos_EquiposAutonomos_EquipoAutonomoId",
                        column: x => x.EquipoAutonomoId,
                        principalTable: "EquiposAutonomos",
                        principalColumn: "EquipoAutonomoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MovimientosEquiposAutonomos_Persona_EncargadoId",
                        column: x => x.EncargadoId,
                        principalTable: "Persona",
                        principalColumn: "PersonaId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "OcupantesVehiculos",
                columns: table => new
                {
                    OcupanteVehiculoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    VehiculoAfectadoId = table.Column<int>(type: "int", nullable: false),
                    DamnificadoSalidaId = table.Column<int>(type: "int", nullable: false),
                    Rol = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OcupantesVehiculos", x => x.OcupanteVehiculoId);
                    table.ForeignKey(
                        name: "FK_OcupantesVehiculos_Damnificados_DamnificadoSalidaId",
                        column: x => x.DamnificadoSalidaId,
                        principalTable: "Damnificados",
                        principalColumn: "Damnificado_SalidaId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Salidas",
                columns: table => new
                {
                    SalidaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TipoEmergencia = table.Column<int>(type: "int", nullable: false),
                    HoraSalida = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    HoraLlegada = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    NumeroParte = table.Column<int>(type: "int", nullable: false),
                    AnioNumeroParte = table.Column<int>(type: "int", nullable: false),
                    Descripcion = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Direccion = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Latitud = table.Column<double>(type: "double", nullable: true),
                    Longitud = table.Column<double>(type: "double", nullable: true),
                    PisoNumero = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Depto = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TipoZona = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NombreSolicitante = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ApellidoSolicitante = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DniSolicitante = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TelefonoSolicitante = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NombreYApellidoReceptor = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SeguroId = table.Column<int>(type: "int", nullable: true),
                    EncargadoId = table.Column<int>(type: "int", nullable: false),
                    QuienLlenoId = table.Column<int>(type: "int", nullable: false),
                    TipoServicio = table.Column<int>(type: "int", nullable: false),
                    Accidente_Tipo = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CondicionesClimaticas = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    OtroCondicion = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FactorClimatico_Tipo = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FactorClimatico_Evacuacion = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Superficie = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CantidadAfectadaFactorClimatico = table.Column<int>(type: "int", nullable: true),
                    DeteccionAutomatica = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    Extintor = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    Hidrante = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    TipoEvacuacion = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TipoSuperficieAfectada = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DetalleSuperficieAfectadaIncendio = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SuperficieAfectadaCausa = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TipoAbertura = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    OtraAbertura = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TipoTecho = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    OtroTecho = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    OtroLugarIncendio = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NombreEstablecimiento = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CantidadPisos = table.Column<int>(type: "int", nullable: true),
                    PisoAfectado = table.Column<int>(type: "int", nullable: true),
                    CantidadAmbientes = table.Column<int>(type: "int", nullable: true),
                    TipoAeronave = table.Column<int>(type: "int", nullable: true),
                    TipoLugar = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IncendioEstablecimientoEducativo_TipoLugar = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IncendioEstablecimientoPublico_TipoLugar = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IncendioForestal_TipoLugar = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IncendioHospitalesYClinicas_TipoLugar = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IncendioIndustria_TipoLugar = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IncendioVivienda_TipoLugar = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Tipo = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Sustancias = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Controlada = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    Venteo = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    DilucionDeVapores = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    Neutralizacion = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    Trasvase = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    OtraAccionesMateriales = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    DetallesAccionesMateriales = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Evacuacion = table.Column<int>(type: "int", nullable: true),
                    Descontaminacion = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    Confinamiento = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    SinAccion = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    OtraAccionesPersonas = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    DetallesAccionesPersonas = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TipoSuperficie = table.Column<int>(type: "int", nullable: true),
                    CantidadAfectadaMaterialPeligroso = table.Column<int>(type: "int", nullable: true),
                    TipoSituacion = table.Column<int>(type: "int", nullable: true),
                    LugarRescateAnimal = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    OtroLugarRescate = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LugarRescatePersona = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TipoOrganizacion = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DetallesColaboFuerzasSeguridad = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NivelDeCapacitacion = table.Column<int>(type: "int", nullable: true),
                    TipoCapacitacion = table.Column<int>(type: "int", nullable: true),
                    DiaHora = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    TipoCapacitacionOtra = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NivelDeCapacitacionOtro = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ServicioEspecialColocaciónDriza_TipoLugar = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ServicioEspecialColocaciónDriza_NombreEstablecimiento = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Detalles = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ServicioEspecialFalsaAlarma_Detalles = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TipoPrevencion = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TipoRepresentacion = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DetallesObito = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NombreEstablecimientoSuministroAgua = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DetallesSuministroAgua = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Salidas", x => x.SalidaId);
                    table.ForeignKey(
                        name: "FK_Salidas_Persona_EncargadoId",
                        column: x => x.EncargadoId,
                        principalTable: "Persona",
                        principalColumn: "PersonaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Salidas_Persona_QuienLlenoId",
                        column: x => x.QuienLlenoId,
                        principalTable: "Persona",
                        principalColumn: "PersonaId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Seguro",
                columns: table => new
                {
                    SeguroId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Tipo = table.Column<int>(type: "int", nullable: false),
                    CompañiaAseguradora = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NumeroDePoliza = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FechaDeVencimineto = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    VehiculoId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seguro", x => x.SeguroId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Vehiculo",
                columns: table => new
                {
                    VehiculoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Discriminador = table.Column<int>(type: "int", nullable: false),
                    Marca = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Modelo = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Año = table.Column<int>(type: "int", nullable: true),
                    Patente = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Tipo = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SeguroId = table.Column<int>(type: "int", nullable: true),
                    NombreYApellidoDuenio = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Color = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Airbag = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    VehiculoAfectado_Observaciones = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SeConoceConductor = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    NumeroMovil = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EncargadoId = table.Column<int>(type: "int", nullable: true),
                    Estado = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ImagenId = table.Column<int>(type: "int", nullable: true),
                    Combustible = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FechaUltimoService = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    FechaProximoService = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Observaciones = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NumeroMotor = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ModeloBomba = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NumeroChasis = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CantidadLitros = table.Column<int>(type: "int", nullable: true),
                    TipoAceite = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MarcaAceite = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CantidadAceite = table.Column<int>(type: "int", nullable: true),
                    ModeloFiltroAire = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MedidasCubiertas = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LibrasCubiertas = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TensionCElectrico = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TipoDireccion = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CajaVelocidades = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MarcaBateria = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FechaUltCambioBateria = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Kilometraje = table.Column<int>(type: "int", nullable: true),
                    EquipoId = table.Column<int>(type: "int", nullable: true),
                    PersonalId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehiculo", x => x.VehiculoId);
                    table.ForeignKey(
                        name: "FK_Vehiculo_Comunicacion_EquipoId",
                        column: x => x.EquipoId,
                        principalTable: "Comunicacion",
                        principalColumn: "ComunicacionId",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Vehiculo_Imagen_ImagenId",
                        column: x => x.ImagenId,
                        principalTable: "Imagen",
                        principalColumn: "ImagenId",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Vehiculo_Persona_EncargadoId",
                        column: x => x.EncargadoId,
                        principalTable: "Persona",
                        principalColumn: "PersonaId",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Vehiculo_Persona_PersonalId",
                        column: x => x.PersonalId,
                        principalTable: "Persona",
                        principalColumn: "PersonaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Vehiculo_Seguro_SeguroId",
                        column: x => x.SeguroId,
                        principalTable: "Seguro",
                        principalColumn: "SeguroId",
                        onDelete: ReferentialAction.SetNull);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_AscensoBomberos_PersonaId",
                table: "AscensoBomberos",
                column: "PersonaId");

            migrationBuilder.CreateIndex(
                name: "IX_bombero_brigada_BrigadaId",
                table: "bombero_brigada",
                column: "BrigadaId");

            migrationBuilder.CreateIndex(
                name: "IX_bombero_dependencia_DependenciaId",
                table: "bombero_dependencia",
                column: "DependenciaId");

            migrationBuilder.CreateIndex(
                name: "IX_BomberosSalida_MovilId",
                table: "BomberosSalida",
                column: "MovilId");

            migrationBuilder.CreateIndex(
                name: "IX_BomberosSalida_PersonaId",
                table: "BomberosSalida",
                column: "PersonaId");

            migrationBuilder.CreateIndex(
                name: "IX_BomberosSalida_SalidaId",
                table: "BomberosSalida",
                column: "SalidaId");

            migrationBuilder.CreateIndex(
                name: "IX_Brigadas_EncargadoPersonaId",
                table: "Brigadas",
                column: "EncargadoPersonaId");

            migrationBuilder.CreateIndex(
                name: "IX_Brigadas_NombreBrigada",
                table: "Brigadas",
                column: "NombreBrigada",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Comunicacion_NroEquipo",
                table: "Comunicacion",
                column: "NroEquipo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Contactos_PersonalId",
                table: "Contactos",
                column: "PersonalId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Damnificados_FuerzaIntervinienteId",
                table: "Damnificados",
                column: "FuerzaIntervinienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Damnificados_SalidaId",
                table: "Damnificados",
                column: "SalidaId");

            migrationBuilder.CreateIndex(
                name: "IX_Dependencias_EncargadoPersonaId",
                table: "Dependencias",
                column: "EncargadoPersonaId");

            migrationBuilder.CreateIndex(
                name: "IX_EmbarcacionesAfectadas_SalidaId",
                table: "EmbarcacionesAfectadas",
                column: "SalidaId");

            migrationBuilder.CreateIndex(
                name: "IX_fuerzaInterviniente_Salidas_FuerzaIntervinienteId",
                table: "fuerzaInterviniente_Salidas",
                column: "FuerzaIntervinienteId");

            migrationBuilder.CreateIndex(
                name: "IX_fuerzaInterviniente_Salidas_SalidaId",
                table: "fuerzaInterviniente_Salidas",
                column: "SalidaId");

            migrationBuilder.CreateIndex(
                name: "IX_HistorialSocios_SocioId",
                table: "HistorialSocios",
                column: "SocioId");

            migrationBuilder.CreateIndex(
                name: "IX_Imagen_LicenciaId",
                table: "Imagen",
                column: "LicenciaId");

            migrationBuilder.CreateIndex(
                name: "IX_Imagen_PersonalId",
                table: "Imagen",
                column: "PersonalId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Imagen_VehiculoId",
                table: "Imagen",
                column: "VehiculoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Licencias_PersonalId",
                table: "Licencias",
                column: "PersonalId");

            migrationBuilder.CreateIndex(
                name: "IX_MovilesSalida_MovilId",
                table: "MovilesSalida",
                column: "MovilId");

            migrationBuilder.CreateIndex(
                name: "IX_MovilesSalida_PersonaId",
                table: "MovilesSalida",
                column: "PersonaId");

            migrationBuilder.CreateIndex(
                name: "IX_MovilesSalida_SalidaId",
                table: "MovilesSalida",
                column: "SalidaId");

            migrationBuilder.CreateIndex(
                name: "IX_Movimientos_DestinoBomberoPersonaId",
                table: "Movimientos",
                column: "DestinoBomberoPersonaId");

            migrationBuilder.CreateIndex(
                name: "IX_Movimientos_DestinoMovilVehiculoId",
                table: "Movimientos",
                column: "DestinoMovilVehiculoId");

            migrationBuilder.CreateIndex(
                name: "IX_Movimientos_MaterialesMaterialId",
                table: "Movimientos",
                column: "MaterialesMaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_MovimientosEquiposAutonomos_DependenciaDestinoId",
                table: "MovimientosEquiposAutonomos",
                column: "DependenciaDestinoId");

            migrationBuilder.CreateIndex(
                name: "IX_MovimientosEquiposAutonomos_EncargadoId",
                table: "MovimientosEquiposAutonomos",
                column: "EncargadoId");

            migrationBuilder.CreateIndex(
                name: "IX_MovimientosEquiposAutonomos_EquipoAutonomoId",
                table: "MovimientosEquiposAutonomos",
                column: "EquipoAutonomoId");

            migrationBuilder.CreateIndex(
                name: "IX_MovimientosEquiposAutonomos_VehiculoDestinoId",
                table: "MovimientosEquiposAutonomos",
                column: "VehiculoDestinoId");

            migrationBuilder.CreateIndex(
                name: "IX_OcupantesVehiculos_DamnificadoSalidaId",
                table: "OcupantesVehiculos",
                column: "DamnificadoSalidaId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OcupantesVehiculos_VehiculoAfectadoId",
                table: "OcupantesVehiculos",
                column: "VehiculoAfectadoId");

            migrationBuilder.CreateIndex(
                name: "IX_PagoSocio_CobradorId",
                table: "PagoSocio",
                column: "CobradorId");

            migrationBuilder.CreateIndex(
                name: "IX_PagoSocio_ConfirmadoPorPersonaId",
                table: "PagoSocio",
                column: "ConfirmadoPorPersonaId");

            migrationBuilder.CreateIndex(
                name: "IX_PagoSocio_SocioId",
                table: "PagoSocio",
                column: "SocioId");

            migrationBuilder.CreateIndex(
                name: "IX_PartesVehiculo_Nombre_Tipo",
                table: "PartesVehiculo",
                columns: new[] { "Nombre", "Tipo" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Persona_EntraId",
                table: "Persona",
                column: "EntraId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Persona_EquipoId",
                table: "Persona",
                column: "EquipoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Persona_NumeroLegajo",
                table: "Persona",
                column: "NumeroLegajo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Salidas_EncargadoId",
                table: "Salidas",
                column: "EncargadoId");

            migrationBuilder.CreateIndex(
                name: "IX_Salidas_QuienLlenoId",
                table: "Salidas",
                column: "QuienLlenoId");

            migrationBuilder.CreateIndex(
                name: "IX_Salidas_SeguroId",
                table: "Salidas",
                column: "SeguroId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sanciones_PersonaId",
                table: "Sanciones",
                column: "PersonaId");

            migrationBuilder.CreateIndex(
                name: "IX_Seguro_VehiculoId",
                table: "Seguro",
                column: "VehiculoId");

            migrationBuilder.CreateIndex(
                name: "IX_Socios_NroSocio",
                table: "Socios",
                column: "NroSocio",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vehiculo_EncargadoId",
                table: "Vehiculo",
                column: "EncargadoId");

            migrationBuilder.CreateIndex(
                name: "IX_Vehiculo_EquipoId",
                table: "Vehiculo",
                column: "EquipoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vehiculo_ImagenId",
                table: "Vehiculo",
                column: "ImagenId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vehiculo_NumeroMovil",
                table: "Vehiculo",
                column: "NumeroMovil",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vehiculo_PersonalId",
                table: "Vehiculo",
                column: "PersonalId");

            migrationBuilder.CreateIndex(
                name: "IX_Vehiculo_SeguroId",
                table: "Vehiculo",
                column: "SeguroId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BomberosSalida_Salidas_SalidaId",
                table: "BomberosSalida",
                column: "SalidaId",
                principalTable: "Salidas",
                principalColumn: "SalidaId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BomberosSalida_Vehiculo_MovilId",
                table: "BomberosSalida",
                column: "MovilId",
                principalTable: "Vehiculo",
                principalColumn: "VehiculoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Damnificados_Salidas_SalidaId",
                table: "Damnificados",
                column: "SalidaId",
                principalTable: "Salidas",
                principalColumn: "SalidaId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Damnificados_fuerzaInterviniente_Salidas_FuerzaInterviniente~",
                table: "Damnificados",
                column: "FuerzaIntervinienteId",
                principalTable: "fuerzaInterviniente_Salidas",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_EmbarcacionesAfectadas_Salidas_SalidaId",
                table: "EmbarcacionesAfectadas",
                column: "SalidaId",
                principalTable: "Salidas",
                principalColumn: "SalidaId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_fuerzaInterviniente_Salidas_Salidas_SalidaId",
                table: "fuerzaInterviniente_Salidas",
                column: "SalidaId",
                principalTable: "Salidas",
                principalColumn: "SalidaId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MovilesSalida_Salidas_SalidaId",
                table: "MovilesSalida",
                column: "SalidaId",
                principalTable: "Salidas",
                principalColumn: "SalidaId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MovilesSalida_Vehiculo_MovilId",
                table: "MovilesSalida",
                column: "MovilId",
                principalTable: "Vehiculo",
                principalColumn: "VehiculoId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Movimientos_Vehiculo_DestinoMovilVehiculoId",
                table: "Movimientos",
                column: "DestinoMovilVehiculoId",
                principalTable: "Vehiculo",
                principalColumn: "VehiculoId");

            migrationBuilder.AddForeignKey(
                name: "FK_MovimientosEquiposAutonomos_Vehiculo_VehiculoDestinoId",
                table: "MovimientosEquiposAutonomos",
                column: "VehiculoDestinoId",
                principalTable: "Vehiculo",
                principalColumn: "VehiculoId");

            migrationBuilder.AddForeignKey(
                name: "FK_OcupantesVehiculos_Vehiculo_VehiculoAfectadoId",
                table: "OcupantesVehiculos",
                column: "VehiculoAfectadoId",
                principalTable: "Vehiculo",
                principalColumn: "VehiculoId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Salidas_Seguro_SeguroId",
                table: "Salidas",
                column: "SeguroId",
                principalTable: "Seguro",
                principalColumn: "SeguroId");

            migrationBuilder.AddForeignKey(
                name: "FK_Seguro_Vehiculo_VehiculoId",
                table: "Seguro",
                column: "VehiculoId",
                principalTable: "Vehiculo",
                principalColumn: "VehiculoId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Imagen_Persona_PersonalId",
                table: "Imagen");

            migrationBuilder.DropForeignKey(
                name: "FK_Licencias_Persona_PersonalId",
                table: "Licencias");

            migrationBuilder.DropForeignKey(
                name: "FK_Vehiculo_Persona_EncargadoId",
                table: "Vehiculo");

            migrationBuilder.DropForeignKey(
                name: "FK_Vehiculo_Persona_PersonalId",
                table: "Vehiculo");

            migrationBuilder.DropForeignKey(
                name: "FK_Seguro_Vehiculo_VehiculoId",
                table: "Seguro");

            migrationBuilder.DropTable(
                name: "AscensoBomberos");

            migrationBuilder.DropTable(
                name: "bombero_brigada");

            migrationBuilder.DropTable(
                name: "bombero_dependencia");

            migrationBuilder.DropTable(
                name: "BomberosSalida");

            migrationBuilder.DropTable(
                name: "Contactos");

            migrationBuilder.DropTable(
                name: "EmbarcacionesAfectadas");

            migrationBuilder.DropTable(
                name: "HistorialSocios");

            migrationBuilder.DropTable(
                name: "MovilesSalida");

            migrationBuilder.DropTable(
                name: "Movimientos");

            migrationBuilder.DropTable(
                name: "MovimientosEquiposAutonomos");

            migrationBuilder.DropTable(
                name: "OcupantesVehiculos");

            migrationBuilder.DropTable(
                name: "PagoSocio");

            migrationBuilder.DropTable(
                name: "PartesVehiculo");

            migrationBuilder.DropTable(
                name: "Sanciones");

            migrationBuilder.DropTable(
                name: "Brigadas");

            migrationBuilder.DropTable(
                name: "Materiales");

            migrationBuilder.DropTable(
                name: "Dependencias");

            migrationBuilder.DropTable(
                name: "EquiposAutonomos");

            migrationBuilder.DropTable(
                name: "Damnificados");

            migrationBuilder.DropTable(
                name: "Socios");

            migrationBuilder.DropTable(
                name: "fuerzaInterviniente_Salidas");

            migrationBuilder.DropTable(
                name: "Fuerzas");

            migrationBuilder.DropTable(
                name: "Salidas");

            migrationBuilder.DropTable(
                name: "Persona");

            migrationBuilder.DropTable(
                name: "Vehiculo");

            migrationBuilder.DropTable(
                name: "Comunicacion");

            migrationBuilder.DropTable(
                name: "Imagen");

            migrationBuilder.DropTable(
                name: "Seguro");

            migrationBuilder.DropTable(
                name: "Licencias");
        }
    }
}
