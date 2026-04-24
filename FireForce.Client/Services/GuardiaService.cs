using Microsoft.EntityFrameworkCore;
using FireForce.Data.Models.Grupos.Guardia;
using FireForce.Data.Models.Personas.Personal;
using FireForce.Data;

namespace FireForce.Client.Services
{
    public interface IGuardiaService
    {
        Task<List<Guardia>> ObtenerTodasLasGuardiasAsync();
        Task<Guardia?> ObtenerGuardiaPorIdAsync(int id);
        Task<List<Bombero?>> ObtenerBomberosDeGuardiaAsync(int guardiaId);
        Task QuitarBomberoDeGuardiaAsync(int brigadaId, int bomberoId);
        Task EditarGuardiaAsync(Guardia guardia);
        Task EliminarGuardiaAsync(int id);
        Task AgregarGuardiaAsync(Guardia guardia);
        Task AgregarBomberoAGuardiaAsync(int guardiaId, int bomberoId);

    }
    public class GuardiaService : IGuardiaService
    {
        private readonly BomberosDbContext _context;
        public GuardiaService(BomberosDbContext context)
        {
            _context = context;
        }



        public async Task<List<Guardia>> ObtenerTodasLasGuardiasAsync()
        {
            return await _context.Guardias
                .Include(g => g.Encargado)
                .ToListAsync();
        }

        public async Task<Guardia?> ObtenerGuardiaPorIdAsync(int id)
        {
            return await _context.Guardias.FindAsync(id);

        }

        public async Task AgregarGuardiaAsync(Guardia guardia)
        {
            _context.Guardias.Add(guardia);
            await _context.SaveChangesAsync();
        }

        public async Task EditarGuardiaAsync(Guardia guardia)
        {
            var guardiaExistente = await _context.Guardias.FindAsync(guardia.GuardiaId);

            if (guardiaExistente != null)
            {
                guardiaExistente.NombreGuardia = guardia.NombreGuardia;
                guardiaExistente.Encargado = guardia.Encargado;

                _context.Guardias.Update(guardiaExistente);
                await _context.SaveChangesAsync();
            }
        }

        public async Task EliminarGuardiaAsync(int id)
        {
            var guardia = await _context.Guardias.FindAsync(id) ;
            if (guardia != null)
            {
                _context.Guardias.Remove(guardia);
                await _context.SaveChangesAsync();
            }
        }



        public async Task QuitarBomberoDeGuardiaAsync(int guardiaId, int bomberoId)
        {
            // Verificar si la relación existe
            var relacion = await _context.Set<Bombero_Guardia>()
                .FirstOrDefaultAsync(bd => bd.GuardiaId == guardiaId && bd.PersonaId == bomberoId);
            if (relacion == null)
                throw new Exception("La relación entre el bombero y la guardia no existe");
            // Eliminar la relación
            _context.Set<Bombero_Guardia>().Remove(relacion);
            await _context.SaveChangesAsync();
        }
        public async Task<List<Bombero?>> ObtenerBomberosDeGuardiaAsync(int guardiaId)
        {
            var guardia = await _context.Guardias
                .Include(g => g.Bomberos) // Aquí Bomberos es de tipo List<Bombero_Guardia>
                .ThenInclude(bg => bg.Bombero) // Incluye los detalles del Bombero
                .FirstOrDefaultAsync(g => g.GuardiaId == guardiaId);

            return guardia.Bomberos
                .Select(bg => bg.Bombero) // Proyecta los bomberos reales
                .ToList() ?? new List<Bombero>();
        }
        public async Task AgregarBomberoAGuardiaAsync(int guardiaId, int bomberoId)
        {
            // Verificar si la guardia existe
            var guardia = await _context.Guardias
                .FirstOrDefaultAsync(d => d.GuardiaId == guardiaId);
            if (guardia == null)
                throw new Exception("Guardia no encontrada");

            // Verificar si el bombero existe
            var bombero = await _context.Bomberos
                .FirstOrDefaultAsync(b => b.PersonaId == bomberoId);

            if (bombero == null)
                throw new Exception("Bombero no encontrado");

            // Verificar si ya existe la relación
            var existeRelacion = await _context.Set<Bombero_Guardia>()
                .AnyAsync(bd => bd.GuardiaId == guardiaId && bd.PersonaId == bomberoId);

            if (existeRelacion)
                throw new Exception("El bombero ya pertenece a la Guardia");
            // Crear la relación y guardarla
            var relacion = new Bombero_Guardia
            {
                GuardiaId = guardiaId,
                PersonaId = bomberoId
            };

            _context.Set<Bombero_Guardia>().Add(relacion);
            await _context.SaveChangesAsync();
        }

    }
}