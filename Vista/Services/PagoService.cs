using Vista.Data;
using Vista.Data.Models.Socios.Componentes.Pagos;
using Microsoft.EntityFrameworkCore;

namespace Vista.Services
{
    public interface IPagoService
    {
        Task AgregarPagoAsync(int SocioId, PagoSocio Pago);
        Task<List<PagoSocio>> ObtenerPagosPorSocioAsync(int SocioId);
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
    }
}
