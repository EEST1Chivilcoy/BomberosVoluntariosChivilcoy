using AntDesign;
using Microsoft.EntityFrameworkCore;
using Vista.Data;
using Vista.Data.Enums.Socios;
using Vista.Data.Models.Imagenes;
using Vista.Data.Models.Personas.Personal;
using Vista.Data.Models.Socios;
using Vista.Data.Models.Socios.Componentes;
using Vista.Helpers;

namespace Vista.Services
{
    public interface IHistorialSocioService
    {
        /// <summary>
        /// Registra un nuevo movimiento en el historial de un socio.
        /// </summary>
        /// <param name="socioId">Identificador único del socio al que se le agrega el movimiento.</param>
        /// <param name="historial">Objeto que contiene la información del movimiento realizado.</param>
        Task CrearMovimientoSocio(int socioId, MovimientoSocio historial);

        /// <summary>
        /// Obtiene el historial completo de movimientos registrados para un socio.
        /// </summary>
        /// <param name="socioId">Identificador único del socio cuyo historial se desea consultar.</param>
        /// <returns>Una lista de <see cref="MovimientoSocio"/> con todos los movimientos asociados al socio.</returns>
        Task<List<MovimientoSocio>> ObtenerHistorialSocio(int socioId);

        /// <summary>
        /// Obtiene el historial de modificaciones en la cuota de un socio.
        /// </summary>
        /// <param name="socioId">Identificador único del socio cuyo historial de cuotas se desea consultar.</param>
        /// <returns>Una lista de <see cref="MovimientoCambioCuota"/> con los cambios de cuota registrados.</returns>
        Task<List<MovimientoCambioCuota>> ObtenerHistorialCuotas(int socioId);

        /// <summary>
        /// Obtiene el historial de cambios de estado de un socio.
        /// </summary>
        /// <param name="socioId">Identificador único del socio cuyo historial de estados se desea consultar.</param>
        /// <returns>Una lista de <see cref="MovimientoCambioEstado"/> con los cambios de estado registrados.</returns>
        Task<List<MovimientoCambioEstado>> ObtenerHistorialEstados(int socioId);

        /// <summary>
        /// Obtiene los períodos en los que el socio estuvo activo.
        /// </summary>
        /// <param name="socioId">Identificador único del socio.</param>
        /// <returns>Lista de movimientos de estado donde el socio estuvo activo.</returns>
        Task<List<MovimientoCambioEstado>> ObtenerPeriodosActivos(int socioId);

        /// <summary>
        /// Obtiene la cuota vigente para una fecha específica.
        /// </summary>
        /// <param name="socioId">Identificador único del socio.</param>
        /// <param name="fecha">Fecha para la cual se desea obtener la cuota vigente.</param>
        /// <returns>El movimiento de cuota vigente en esa fecha, o null si no existe.</returns>
        Task<MovimientoCambioCuota?> ObtenerCuotaVigenteEnFecha(int socioId, DateTime fecha);
    }

    public class HistorialSocioService : IHistorialSocioService
    {
        private readonly BomberosDbContext _context;

        public HistorialSocioService(BomberosDbContext context)
        {
            _context = context;
        }

        public async Task<List<MovimientoSocio>> ObtenerHistorialSocio(int socioId)
        {
            return await _context.HistorialSocios
                .Where(h => h.SocioId == socioId)
                .OrderByDescending(h => h.FechaDesde)
                .ToListAsync();
        }

        public async Task<List<MovimientoCambioCuota>> ObtenerHistorialCuotas(int socioId)
        {
            return await _context.HistorialSocios
                .OfType<MovimientoCambioCuota>()
                .Where(h => h.SocioId == socioId)
                .OrderBy(h => h.FechaDesde)
                .ToListAsync();
        }

        public async Task<List<MovimientoCambioEstado>> ObtenerHistorialEstados(int socioId)
        {
            return await _context.HistorialSocios
                .OfType<MovimientoCambioEstado>()
                .Where(h => h.SocioId == socioId)
                .OrderBy(h => h.FechaDesde)
                .ToListAsync();
        }

        public async Task<List<MovimientoCambioEstado>> ObtenerPeriodosActivos(int socioId)
        {
            return await _context.HistorialSocios
                .OfType<MovimientoCambioEstado>()
                .Where(h => h.SocioId == socioId && h.Estado == TipoEstadoSocio.Activo)
                .OrderBy(h => h.FechaDesde)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<MovimientoCambioCuota?> ObtenerCuotaVigenteEnFecha(int socioId, DateTime fecha)
        {
            return await _context.HistorialSocios
                .OfType<MovimientoCambioCuota>()
                .Where(h => h.SocioId == socioId 
                    && h.FechaDesde <= fecha 
                    && (h.FechaHasta == null || h.FechaHasta >= fecha))
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        public async Task CrearMovimientoSocio(int socioId, MovimientoSocio historial)
        {
            // --- 1. Validaciones ---

            // --- Paso A: Validaciones "Baratas" (en memoria) ---
            if (historial == null)
            {
                throw new ArgumentNullException(nameof(historial), "El historial no puede ser nulo.");
            }

            // Validar el historial usando el helper (Data Annotations)
            ValidationHelper.Validar(historial);

            // --- Paso B: Validaciones "Caras" (base de datos) ---
            if (!await _context.Socios.AnyAsync(s => s.Id == socioId))
            {
                throw new ArgumentException($"No existe un socio con Id {socioId}.", nameof(socioId));
            }

            // --- 2. Inicio de la Transacción ---
            // Esta será la transacción "principal" que controlará todo.
            await using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                // --- Paso A: Ajustar el Modelo ---
                historial.SocioId = socioId;
                historial.FechaDesde = DateTime.UtcNow;
                historial.FechaHasta = null; // El nuevo movimiento queda abierto

                // --- Paso B: Cerrar movimiento anterior del mismo tipo ---
                await CerrarMovimientoAnteriorAsync(socioId, historial);

                // --- Paso C: Guardamos el nuevo movimiento ---
                await _context.HistorialSocios.AddAsync(historial);
                await _context.SaveChangesAsync();

                // --- Paso D: Confirmar la Transacción ---
                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                // --- Manejo de Error ---
                // Si CUALQUIER operación falla (el primer SaveChanges,
                // la lógica del service, o el segundo SaveChanges dentro del service),
                // revertimos TODA la operación.
                await transaction.RollbackAsync();

                // Limpiar el contexto para evitar conflictos futuros
                _context.ChangeTracker.Clear();

                // Lanza una excepción genérica o la 'ex' original
                // para que la capa superior sepa que algo falló.
                if (ex is DbUpdateException)
                {
                    throw new Exception("Error al guardar en la base de datos. Verifique datos duplicados.", ex);
                }

                // Re-lanza la excepción (ej. la ValidationException del service)
                throw;
            }
        }

        /// <summary>
        /// Cierra el movimiento anterior del mismo tipo (si existe) estableciendo FechaHasta.
        /// </summary>
        private async Task CerrarMovimientoAnteriorAsync(int socioId, MovimientoSocio nuevoMovimiento)
        {
            MovimientoSocio? movimientoAnterior = null;

            // Buscar el movimiento vigente del mismo tipo
            if (nuevoMovimiento is MovimientoCambioEstado)
            {
                movimientoAnterior = await _context.HistorialSocios
                    .OfType<MovimientoCambioEstado>()
                    .Where(m => m.SocioId == socioId && m.FechaHasta == null)
                    .FirstOrDefaultAsync();
            }
            else if (nuevoMovimiento is MovimientoCambioCuota)
            {
                movimientoAnterior = await _context.HistorialSocios
                    .OfType<MovimientoCambioCuota>()
                    .Where(m => m.SocioId == socioId && m.FechaHasta == null)
                    .FirstOrDefaultAsync();
            }

            // Si existe un movimiento anterior vigente, cerrarlo
            if (movimientoAnterior != null)
            {
                movimientoAnterior.FechaHasta = nuevoMovimiento.FechaDesde;
            }
        }
    }
}