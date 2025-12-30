using AntDesign;
using Microsoft.EntityFrameworkCore;
using Vista.Data;
using Vista.Data.Models.Imagenes;
using Vista.Data.Models.Personas.Personal;
using Vista.Data.Models.Socios.Componentes;
using Vista.Helpers;

namespace Vista.Services
{
    public interface IHistorialSocioService
    {
        Task CrearMovimientoSocio(int socioId, MovimientoSocio historial);
    }

    public class HistorialSocioService : IHistorialSocioService
    {
        private readonly BomberosDbContext _context;

        public HistorialSocioService(BomberosDbContext context)
        {
            _context = context;
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