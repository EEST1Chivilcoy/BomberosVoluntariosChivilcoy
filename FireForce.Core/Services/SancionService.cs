using Microsoft.EntityFrameworkCore;
using FireForce.Core.Data;
using FireForce.Core.Data.Models.Personas.Personal.Componentes;

namespace FireForce.Core.Services
{
    public interface ISancionService
    {
        Task<List<Sancion>> ObtenerTodasLasSancionesAsync();
    }

    public class SancionService : ISancionService
    {
        private readonly BomberosDbContext _context;

        public SancionService(BomberosDbContext context)
        {
            _context = context;
        }

        public async Task<List<Sancion>> ObtenerTodasLasSancionesAsync()
        {
            return await _context.Sanciones.AsNoTracking().ToListAsync();
        }


    }
}
