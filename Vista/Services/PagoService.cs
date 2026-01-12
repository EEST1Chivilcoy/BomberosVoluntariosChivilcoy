using Vista.Data;
using Vista.Data.Enums.Socios;
using Vista.Data.Models.Socios.Componentes.Pagos;
using Microsoft.EntityFrameworkCore;
using Vista.Helpers;

namespace Vista.Services
{
    public interface IPagoService
    {
        Task AgregarPagoAsync(int SocioId, PagoSocio Pago);
        Task<List<PagoSocio>> ObtenerPagosPorSocioAsync(int SocioId);

        /// <summary>
        /// Obtiene los pagos confirmados de un socio.
        /// </summary>
        /// <param name="socioId">Identificador único del socio.</param>
        /// <returns>Lista de pagos confirmados del socio.</returns>
        Task<List<PagoSocio>> ObtenerPagosConfirmadosPorSocioAsync(int socioId);

        /// <summary>
        /// Calcula el total de pagos confirmados de un socio.
        /// </summary>
        /// <param name="socioId">Identificador único del socio.</param>
        /// <returns>Suma total de los montos de pagos confirmados.</returns>
        Task<decimal> ObtenerTotalPagosConfirmadosAsync(int socioId);
    }

    public class PagoService : IPagoService
    {
        private readonly BomberosDbContext _context;

        public PagoService(BomberosDbContext context)
        {
            _context = context;
        }

        public async Task AgregarPagoAsync(int SocioId, PagoSocio Pago)
        {
            // --- 1. Validaciones ---

            // --- Paso A: Validaciones "Baratas" (en memoria) ---
            if (Pago == null)
            {
                throw new ArgumentNullException(nameof(Pago), "El pago no puede ser nulo.");
            }

            if (SocioId == 0)
            {
                throw new ArgumentException("El ID del socio no puede ser cero.", nameof(SocioId));
            }

            ValidationHelper.Validar(Pago);

            // --- 2. Inicio de la Transacción ---
            // Esta será la transacción "principal" que controlará todo.
            await using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                // --- Paso B: Validaciones "Caras" (base de datos) ---

                var socioExistente = await _context.Socios
                    .AsNoTracking()
                    .AnyAsync(s => s.Id == SocioId);

                if (!socioExistente)
                {
                    throw new KeyNotFoundException($"No se encontró un socio con ID {SocioId}.");
                }

                // --- 3. Guardar el Pago ---

                // Guardamos el pago dentro de la transacción.
                Pago.SocioId = SocioId;
                _context.PagoSocio.Add(Pago);
                await _context.SaveChangesAsync();

                // --- 4. Confirmar la Transacción ---
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

        public async Task<List<PagoSocio>> ObtenerPagosPorSocioAsync(int SocioId)
        {
            return await _context.PagoSocio
                .Where(p => p.SocioId == SocioId)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<List<PagoSocio>> ObtenerPagosConfirmadosPorSocioAsync(int socioId)
        {
            return await _context.PagoSocio
                .Where(p => p.SocioId == socioId && p.Estado == EstadoPago.Confirmado)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<decimal> ObtenerTotalPagosConfirmadosAsync(int socioId)
        {
            return (decimal)await _context.PagoSocio
                .Where(p => p.SocioId == socioId && p.Estado == EstadoPago.Confirmado)
                .SumAsync(p => p.Monto);
        }
    }
}
