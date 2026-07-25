using FireForce.Client.Helpers;
using FireForce.Data;
using FireForce.Data.Models.Grupos.Dependencias;
using FireForce.Data.Models.Otros.Firmas;
using FireForce.Data.Models.Personas;
using FireForce.Data.Models.Personas.Personal;
using FireForce.Data.Models.Vehiculos.Flota;
using FireForce.Shared.Enums.Firmas;
using FireForce.Shared.Extensions;
using Microsoft.EntityFrameworkCore;

namespace FireForce.Client.Services
{
    /// <summary>
    /// Gestiona el ciclo de vida completo de un incidente: alta, aprobación, rechazo,
    /// derivación mediante tareas y la bitácora de movimientos.
    /// </summary>
    /// <remarks>
    /// Las tareas se manejan desde acá y no desde un servicio aparte porque no tienen vida propia:
    /// existen siempre dentro de un incidente y derivar implica tocar el incidente y la tarea en la
    /// misma operación.
    /// </remarks>
    public interface IIncidenteService
    {
        /// <summary>Lista los incidentes, opcionalmente filtrados por estado.</summary>
        Task<List<IncidenteBase>> ObtenerIncidentesAsync(EstadoIncidente? estado = null);

        /// <summary>Obtiene un incidente con su solicitante, encargado, tareas y bitácora.</summary>
        Task<IncidenteBase> ObtenerIncidentePorIdAsync(int incidenteId, bool asNoTracking = false);

        /// <summary>
        /// Registra un incidente nuevo. El encargado se resuelve solo a partir del móvil o de la
        /// dependencia afectada; no se recibe por parámetro.
        /// </summary>
        Task CrearIncidenteAsync(IncidenteBase incidente);

        /// <summary>
        /// Aprueba el incidente y otorga <paramref name="cantidadFirmas"/> firmas al solicitante.
        /// </summary>
        Task AprobarIncidenteAsync(int incidenteId, int autorId, int cantidadFirmas, string? observaciones = null);

        /// <summary>Rechaza el incidente. No otorga firmas.</summary>
        Task RechazarIncidenteAsync(int incidenteId, int autorId, string? observaciones = null);

        /// <summary>
        /// Deriva el incidente a otro departamento creando una tarea. Si se indica
        /// <paramref name="tareaPadreId"/>, la nueva tarea queda como subtarea de esa.
        /// </summary>
        Task<Tarea> DerivarIncidenteAsync(int incidenteId, int autorId, int dependenciaDestinoId,
            string nombre, string descripcion, int? tareaPadreId = null);

        /// <summary>Cambia el estado de una tarea y lo registra en la bitácora del incidente.</summary>
        Task CambiarEstadoTareaAsync(int tareaId, EstadoTarea nuevoEstado, int autorId, string? observaciones = null);

        /// <summary>Móviles y embarcaciones de la flota que tienen encargado asignado.</summary>
        Task<List<VehiculoSalida>> ObtenerMovilesDisponiblesAsync();

        /// <summary>Dependencias que pueden recibir un incidente o una derivación.</summary>
        Task<List<Dependencia>> ObtenerDependenciasAsync();

        /// <summary>Personas que pueden figurar como solicitantes.</summary>
        Task<List<Persona>> ObtenerSolicitantesAsync();
    }

    public class IncidenteService : IIncidenteService
    {
        private readonly BomberosDbContext _context;

        public IncidenteService(BomberosDbContext context)
        {
            _context = context;
        }

        // ---------------------------------------------------------------------
        //  Lecturas
        // ---------------------------------------------------------------------

        public async Task<List<IncidenteBase>> ObtenerIncidentesAsync(EstadoIncidente? estado = null)
        {
            IQueryable<IncidenteBase> consulta = _context.Incidentes
                .Include(i => i.Solicitante)
                .Include(i => i.Encargado)
                .Include(i => i.Tareas)
                .AsNoTracking();

            if (estado.HasValue)
            {
                consulta = consulta.Where(i => i.Estado == estado.Value);
            }

            var incidentes = await consulta.OrderByDescending(i => i.Fecha).ToListAsync();

            // Los datos del objeto afectado dependen del subtipo, así que se cargan aparte para no
            // arrastrar Include condicionales sobre la jerarquía.
            await CargarObjetoAfectadoAsync(incidentes);

            return incidentes;
        }

        public async Task<IncidenteBase> ObtenerIncidentePorIdAsync(int incidenteId, bool asNoTracking = false)
        {
            IQueryable<IncidenteBase> consulta = _context.Incidentes
                .Include(i => i.Solicitante)
                .Include(i => i.Encargado)
                .Include(i => i.Movimientos).ThenInclude(m => m.Autor)
                .Include(i => i.Tareas).ThenInclude(t => t.Dependencia)
                .Include(i => i.Tareas).ThenInclude(t => t.Encargado);

            if (asNoTracking)
            {
                consulta = consulta.AsNoTracking();
            }

            var incidente = await consulta.FirstOrDefaultAsync(i => i.Id == incidenteId)
                ?? throw new KeyNotFoundException($"No se encontró el incidente con el ID {incidenteId}.");

            await CargarObjetoAfectadoAsync(new List<IncidenteBase> { incidente });

            return incidente;
        }

        public async Task<List<VehiculoSalida>> ObtenerMovilesDisponiblesAsync()
        {
            // El DbSet de VehiculoSalida ya restringe a la flota propia (móviles y embarcaciones):
            // deja afuera los vehículos afectados y los particulares de la misma tabla TPH.
            return await _context.VehiculoSalidas
                .Include(v => v.Encargado)
                .Where(v => v.EncargadoId != null)
                .OrderBy(v => v.NumeroMovil)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<List<Dependencia>> ObtenerDependenciasAsync()
        {
            return await _context.Dependencias
                .Include(d => d.Encargado)
                .OrderBy(d => d.NombreDependencia)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<List<Persona>> ObtenerSolicitantesAsync()
        {
            return await _context.Set<Persona>()
                .OrderBy(p => p.Apellido).ThenBy(p => p.Nombre)
                .AsNoTracking()
                .ToListAsync();
        }

        // ---------------------------------------------------------------------
        //  Alta
        // ---------------------------------------------------------------------

        public async Task CrearIncidenteAsync(IncidenteBase incidente)
        {
            ArgumentNullException.ThrowIfNull(incidente);

            // --- Paso A: validaciones baratas (en memoria) ---
            incidente.Fecha = DateTime.Now;
            incidente.Estado = EstadoIncidente.Pendiente;
            incidente.CantidadFirmas = 0;

            ValidationHelper.Validar(incidente);

            // --- Paso B: validaciones caras (contra la base) ---
            var solicitante = await _context.Set<Persona>()
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.PersonaId == incidente.SolicitanteId)
                ?? throw new KeyNotFoundException($"No se encontró la persona con el ID {incidente.SolicitanteId}.");

            // Regla de negocio: una persona que no es bombero solo puede reportar novedades.
            if (solicitante is not Bombero && incidente.Categoria != CategoriaIncidente.Novedad)
            {
                throw new InvalidOperationException(
                    "Solo los bomberos pueden registrar incidentes de una categoría distinta de Novedad.");
            }

            // El encargado sale del móvil o de la dependencia afectada, nunca lo elige quien carga.
            incidente.EncargadoId = incidente switch
            {
                IncidenteMovil movil => await ResolverEncargadoDeMovilAsync(movil.VehiculoId),
                IncidenteDependencia dependencia => await ResolverEncargadoDeDependenciaAsync(dependencia.DependenciaId),
                _ => throw new NotSupportedException($"Tipo de incidente no soportado: {incidente.GetType().Name}.")
            };

            incidente.Movimientos.Add(new MovimientoIncidente
            {
                Tipo = TipoMovimiento.Creacion,
                AutorId = incidente.SolicitanteId,
                Descripcion = $"Se registró el incidente ({incidente.Categoria.GetDisplayName()})."
            });

            _context.Incidentes.Add(incidente);
            await _context.SaveChangesAsync();
        }

        // ---------------------------------------------------------------------
        //  Resolución
        // ---------------------------------------------------------------------

        public async Task AprobarIncidenteAsync(int incidenteId, int autorId, int cantidadFirmas, string? observaciones = null)
        {
            if (cantidadFirmas < 0)
            {
                throw new ArgumentException("La cantidad de firmas no puede ser negativa.", nameof(cantidadFirmas));
            }

            var incidente = await ObtenerParaResolverAsync(incidenteId);

            if (cantidadFirmas > 0)
            {
                // Reglas de negocio de las firmas. Se validan las dos por separado a propósito:
                // hoy una implica la otra, pero si mañana se agrega una categoría nueva que no sea
                // Novedad, la restricción de "solo bomberos" tiene que seguir valiendo igual.
                if (incidente.Categoria == CategoriaIncidente.Novedad)
                {
                    throw new InvalidOperationException("Las novedades no otorgan firmas.");
                }

                if (incidente.Solicitante is not Bombero)
                {
                    throw new InvalidOperationException("Solo se otorgan firmas a un bombero.");
                }
            }

            incidente.Estado = EstadoIncidente.Aprobado;
            incidente.CantidadFirmas = cantidadFirmas;

            var detalle = cantidadFirmas > 0
                ? $"Se aprobó el incidente y se otorgaron {cantidadFirmas} firma(s)."
                : "Se aprobó el incidente sin otorgar firmas.";

            incidente.Movimientos.Add(new MovimientoIncidente
            {
                Tipo = TipoMovimiento.CambioDeEstado,
                AutorId = autorId,
                Descripcion = Combinar(detalle, observaciones)
            });

            await _context.SaveChangesAsync();
        }

        public async Task RechazarIncidenteAsync(int incidenteId, int autorId, string? observaciones = null)
        {
            var incidente = await ObtenerParaResolverAsync(incidenteId);

            incidente.Estado = EstadoIncidente.Desaprobado;
            incidente.CantidadFirmas = 0;

            incidente.Movimientos.Add(new MovimientoIncidente
            {
                Tipo = TipoMovimiento.CambioDeEstado,
                AutorId = autorId,
                Descripcion = Combinar("Se rechazó el incidente.", observaciones)
            });

            await _context.SaveChangesAsync();
        }

        // ---------------------------------------------------------------------
        //  Derivación (tareas)
        // ---------------------------------------------------------------------

        public async Task<Tarea> DerivarIncidenteAsync(int incidenteId, int autorId, int dependenciaDestinoId,
            string nombre, string descripcion, int? tareaPadreId = null)
        {
            if (string.IsNullOrWhiteSpace(nombre))
            {
                throw new ArgumentException("El nombre de la tarea es obligatorio.", nameof(nombre));
            }

            if (string.IsNullOrWhiteSpace(descripcion))
            {
                throw new ArgumentException("La descripción de la tarea es obligatoria.", nameof(descripcion));
            }

            var incidente = await ObtenerParaResolverAsync(incidenteId);

            var dependencia = await _context.Dependencias
                .AsNoTracking()
                .FirstOrDefaultAsync(d => d.DependenciaId == dependenciaDestinoId)
                ?? throw new KeyNotFoundException($"No se encontró la dependencia con el ID {dependenciaDestinoId}.");

            if (tareaPadreId.HasValue)
            {
                var perteneceAlIncidente = await _context.Tareas
                    .AnyAsync(t => t.Id == tareaPadreId.Value && t.IncidenteId == incidenteId);

                if (!perteneceAlIncidente)
                {
                    throw new InvalidOperationException(
                        "La tarea padre indicada no pertenece a este incidente.");
                }
            }

            var tarea = new Tarea
            {
                Nombre = nombre.Trim(),
                Descripcion = descripcion.Trim(),
                Estado = EstadoTarea.Pendiente,
                IncidenteId = incidenteId,
                DependenciaId = dependencia.DependenciaId,
                EncargadoId = dependencia.EncargadoPersonaId,
                TareaPadreId = tareaPadreId
            };

            ValidationHelper.Validar(tarea);

            _context.Tareas.Add(tarea);

            // Derivar significa que el incidente pasó a estar en curso en otro departamento.
            if (incidente.Estado == EstadoIncidente.Pendiente)
            {
                incidente.Estado = EstadoIncidente.EnProceso;
            }

            incidente.Movimientos.Add(new MovimientoIncidente
            {
                Tipo = TipoMovimiento.CreacionDeTarea,
                AutorId = autorId,
                Descripcion = $"Se derivó a {dependencia.NombreDependencia}: {tarea.Nombre}."
            });

            await _context.SaveChangesAsync();

            return tarea;
        }

        public async Task CambiarEstadoTareaAsync(int tareaId, EstadoTarea nuevoEstado, int autorId, string? observaciones = null)
        {
            var tarea = await _context.Tareas
                .Include(t => t.Dependencia)
                .Include(t => t.Incidente).ThenInclude(i => i.Movimientos)
                .FirstOrDefaultAsync(t => t.Id == tareaId)
                ?? throw new KeyNotFoundException($"No se encontró la tarea con el ID {tareaId}.");

            if (tarea.Estado == nuevoEstado)
            {
                return;
            }

            var estadoAnterior = tarea.Estado;
            tarea.Estado = nuevoEstado;

            tarea.Incidente.Movimientos.Add(new MovimientoIncidente
            {
                Tipo = TipoMovimiento.CambioDeEstado,
                AutorId = autorId,
                Descripcion = Combinar(
                    $"La tarea \"{tarea.Nombre}\" pasó de {estadoAnterior.GetDisplayName()} a {nuevoEstado.GetDisplayName()}.",
                    observaciones)
            });

            await _context.SaveChangesAsync();
        }

        // ---------------------------------------------------------------------
        //  Apoyo
        // ---------------------------------------------------------------------

        private async Task<IncidenteBase> ObtenerParaResolverAsync(int incidenteId)
        {
            var incidente = await _context.Incidentes
                .Include(i => i.Solicitante)
                .Include(i => i.Movimientos)
                .FirstOrDefaultAsync(i => i.Id == incidenteId)
                ?? throw new KeyNotFoundException($"No se encontró el incidente con el ID {incidenteId}.");

            if (incidente.Estado is EstadoIncidente.Aprobado or EstadoIncidente.Desaprobado)
            {
                throw new InvalidOperationException(
                    $"El incidente ya fue resuelto ({incidente.Estado.GetDisplayName()}) y no admite cambios.");
            }

            return incidente;
        }

        private async Task<int> ResolverEncargadoDeMovilAsync(int vehiculoId)
        {
            // Consultar por el DbSet de VehiculoSalida garantiza que sea un móvil o una embarcación
            // de la flota: los vehículos afectados y los particulares comparten tabla pero no subtipo.
            var vehiculo = await _context.VehiculoSalidas
                .AsNoTracking()
                .FirstOrDefaultAsync(v => v.VehiculoId == vehiculoId)
                ?? throw new KeyNotFoundException(
                    $"No se encontró un móvil o embarcación de la flota con el ID {vehiculoId}.");

            if (vehiculo.EncargadoId is null)
            {
                throw new InvalidOperationException(
                    $"El móvil {vehiculo.NumeroMovil} no tiene un encargado asignado, " +
                    "así que no se le pueden registrar incidentes. Asignale un encargado primero.");
            }

            return vehiculo.EncargadoId.Value;
        }

        private async Task<int> ResolverEncargadoDeDependenciaAsync(int dependenciaId)
        {
            var dependencia = await _context.Dependencias
                .AsNoTracking()
                .FirstOrDefaultAsync(d => d.DependenciaId == dependenciaId)
                ?? throw new KeyNotFoundException($"No se encontró la dependencia con el ID {dependenciaId}.");

            return dependencia.EncargadoPersonaId;
        }

        /// <summary>
        /// Carga el móvil o la dependencia de cada incidente según su subtipo.
        /// </summary>
        private async Task CargarObjetoAfectadoAsync(List<IncidenteBase> incidentes)
        {
            var moviles = incidentes.OfType<IncidenteMovil>().ToList();
            if (moviles.Count > 0)
            {
                var ids = moviles.Select(i => i.VehiculoId).Distinct().ToList();
                var vehiculos = await _context.VehiculoSalidas
                    .Where(v => ids.Contains(v.VehiculoId))
                    .AsNoTracking()
                    .ToDictionaryAsync(v => v.VehiculoId);

                foreach (var incidente in moviles)
                {
                    if (vehiculos.TryGetValue(incidente.VehiculoId, out var vehiculo))
                    {
                        incidente.Vehiculo = vehiculo;
                    }
                }
            }

            var dependencias = incidentes.OfType<IncidenteDependencia>().ToList();
            if (dependencias.Count > 0)
            {
                var ids = dependencias.Select(i => i.DependenciaId).Distinct().ToList();
                var registros = await _context.Dependencias
                    .Where(d => ids.Contains(d.DependenciaId))
                    .AsNoTracking()
                    .ToDictionaryAsync(d => d.DependenciaId);

                foreach (var incidente in dependencias)
                {
                    if (registros.TryGetValue(incidente.DependenciaId, out var dependencia))
                    {
                        incidente.Dependencia = dependencia;
                    }
                }
            }
        }

        private static string Combinar(string detalle, string? observaciones) =>
            string.IsNullOrWhiteSpace(observaciones) ? detalle : $"{detalle} {observaciones.Trim()}";
    }
}
