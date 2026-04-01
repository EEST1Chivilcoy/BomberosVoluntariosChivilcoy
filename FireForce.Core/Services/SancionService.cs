using Microsoft.EntityFrameworkCore;
using Vista.Data;
using Vista.Data.Models.Personas.Personal.Componentes;

namespace Vista.Services
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
