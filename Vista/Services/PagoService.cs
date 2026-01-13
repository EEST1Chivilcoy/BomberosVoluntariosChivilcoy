using Vista.Data;
using Vista.Data.Enums.Socios;
using Vista.Data.Models.Socios.Componentes.Pagos;
using Microsoft.EntityFrameworkCore;
using Vista.Helpers;

namespace Vista.Services
{
    public interface IPagoService
    {
        /// <summary>
        /// Agrega un nuevo pago para un socio específico de manera asincrónica.
        /// </summary>
        /// <param name="SocioId">Identificador único del socio al que se le registrará el pago.</param>
        /// <param name="Pago">Objeto que contiene la información detallada del pago realizado por el socio.</param>
        /// <returns>Una tarea que representa la operación asincrónica de registro del pago.</returns>
        Task AgregarPagoAsync(int SocioId, PagoSocio Pago);

        /// <summary>
        /// Obtiene de manera asincrónica la lista de pagos de socios filtrados por su estado.
        /// </summary>
        /// <param name="estado">Estado del pago (por ejemplo: pendiente, aprobado, rechazado) que se desea consultar.</param>
        /// <returns>Una tarea que representa la operación asincrónica y contiene la lista de pagos de socios con el estado especificado.</returns>
        Task<List<PagoSocio>> ObtenerPagosPorEstadoAsync(EstadoPago estado);

        /// <summary>
        /// Obtiene de manera asincrónica todos los pagos de socios registrados en el sistema.
        /// </summary>
        /// <returns>Una tarea que representa la operación asincrónica y contiene la lista completa de pagos de socios.</returns>
        Task<List<PagoSocio>> ObtenerTodosLosPagosAsync();

        /// <summary>
        /// Obtiene todos los pagos asociados a un socio específico.
        /// </summary>
        /// <param name="SocioId">Identificador único del socio.</param>
        /// <returns>Lista de pagos confirmados del socio.</returns>
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

        /// <summary>
        /// Cambia de manera asincrónica el estado de un pago específico en el sistema.
        /// </summary>
        /// <param name="pagoId">Identificador único del pago cuyo estado se desea modificar.</param>
        /// <param name="estado">Nuevo estado que se asignará al pago (por ejemplo: pendiente, aprobado, rechazado).</param>
        /// <param name="razon">Razón o comentario opcional que justifica el cambio de estado del pago.</param>
        /// <returns>Una tarea que representa la operación asincrónica de actualización del estado del pago.</returns>
        Task CambiarEstadoPago(int pagoId, EstadoPago estado, string? razon);
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

        public async Task<List<PagoSocio>> ObtenerPagosPorEstadoAsync(EstadoPago estado)
        {
            return await _context.PagoSocio
                .Where(p => p.Estado == estado)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<List<PagoSocio>> ObtenerTodosLosPagosAsync()
        {
            return await _context.PagoSocio
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task CambiarEstadoPago(int pagoId, EstadoPago estado, string? razon)
        {
            var pago = await _context.PagoSocio.FindAsync(pagoId);

            if (pago == null)
            {
                throw new KeyNotFoundException($"No se encontró un pago con ID {pagoId}.");
            }

            if (pago.Estado == estado)
            {
                throw new InvalidOperationException("El pago ya tiene el estado especificado.");
            }

            pago.Estado = estado;
            pago.FechaConfirmadoORechazado = DateTime.UtcNow;

            // Si el estado es Rechazado, la razón es obligatoria y debe ser registrada.
            if(estado == EstadoPago.Rechazado)
            {
                if (string.IsNullOrWhiteSpace(razon))
                {
                    throw new ArgumentException("La razón es obligatoria al rechazar un pago.", nameof(razon));
                }

                pago.RazonRechazo = razon;
            }

            await _context.SaveChangesAsync();
        }
    }
}
