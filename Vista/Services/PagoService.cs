using Vista.Data;
using Vista.Data.Enums.Socios;
using Vista.Data.Models.Socios.Componentes.Pagos;
using Microsoft.EntityFrameworkCore;

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
            Pago.SocioId = SocioId;
            _context.PagoSocio.Add(Pago);
            await _context.SaveChangesAsync();
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
