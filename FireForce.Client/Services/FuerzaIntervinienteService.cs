using Microsoft.EntityFrameworkCore;
using FireForce.Core.Data;
using FireForce.Core.Data.Models.Grupos.FuerzasIntervinientes;

namespace FireForce.Client.Services
{
    public interface IFuerzaIntervinienteService
    {
        Task<List<FuerzaInterviniente>> ObtenerTodasLasFuerzasAsync();
        Task<FuerzaInterviniente?> ObtenerFuerzaPorIdAsync(int id);
        Task AgregarFuerzaAsync(FuerzaInterviniente fuerza);
        Task EditarFuerzaAsync(FuerzaInterviniente fuerza);
        Task EliminarFuerzaAsync(int id);
    }

    public class FuerzaIntervinienteService : IFuerzaIntervinienteService
    {
        private readonly BomberosDbContext _context;

        public FuerzaIntervinienteService(BomberosDbContext context)
        {
            _context = context;
        }

        public async Task<List<FuerzaInterviniente>> ObtenerTodasLasFuerzasAsync()
        {
            return await _context.Fuerzas
                .OrderBy(f => f.NombreFuerza) // Ordena por nombre
                .AsNoTracking() // Evita el seguimiento de EF Core
                .ToListAsync();
        }

        public async Task<FuerzaInterviniente?> ObtenerFuerzaPorIdAsync(int id)
        {
            return await _context.Fuerzas
                .AsNoTracking() // Optimiza la consulta
                .FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task AgregarFuerzaAsync(FuerzaInterviniente fuerza)
        {
            _context.Fuerzas.Add(fuerza);
            await _context.SaveChangesAsync();
        }

        public async Task EditarFuerzaAsync(FuerzaInterviniente fuerza)
        {
            _context.Fuerzas.Attach(fuerza);
            _context.Entry(fuerza).Property(f => f.NombreFuerza).IsModified = true;
            await _context.SaveChangesAsync();
        }

        public async Task EliminarFuerzaAsync(int id)
        {
            var fuerza = await _context.Fuerzas.FindAsync(id);
            if (fuerza != null)
            {
                _context.Fuerzas.Remove(fuerza);
                await _context.SaveChangesAsync();
            }
        }
    }
}
