using Microsoft.EntityFrameworkCore;
using System;
using Vista.Data;
using Vista.Data.Models.Grupos.Dependencias;
using Vista.Data.Models.Personas.Personal;

namespace Vista.Services
{
    public interface IDependenciaService
    {
        Task<List<Dependencia>> ObtenerTodasLasDependenciasAsync();
        Task<Dependencia?> ObtenerDependenciaPorIdAsync(int id);
        Task<List<Bombero?>> ObtenerBomberosDeDependenciaAsync(int dependenciaId);
        Task AgregarDependenciaAsync(Dependencia dependencia);
        Task EditarDependenciaAsync(Dependencia dependencia);
        Task EliminarDependenciaAsync(int id);
        Task AgregarBomberoADependenciaAsync(int dependenciaId, int bomberoId);
        Task QuitarBomberoDeDependenciaAsync(int dependenciaId, int bomberoId);
        Task<bool> ComprobarBomberoEnDependenciaAsync(int dependenciaId, int bomberoId);
    }

    public class DependenciaService : IDependenciaService
    {
        private readonly BomberosDbContext _context;

        public DependenciaService(BomberosDbContext context)
        {
            _context = context;
        }

        public async Task<List<Dependencia>> ObtenerTodasLasDependenciasAsync()
        {
            return await _context.Dependencias.Include(d => d.Encargado).ToListAsync();
        }

        public async Task<Dependencia?> ObtenerDependenciaPorIdAsync(int id)
        {
            return await _context.Dependencias.FindAsync(id);
        }

        public async Task AgregarDependenciaAsync(Dependencia dependencia)
        {
            _context.Dependencias.Add(dependencia);
            await _context.SaveChangesAsync();
        }

        public async Task EditarDependenciaAsync(Dependencia dependencia)
        {
            var dependenciaExistente = await _context.Dependencias.FindAsync(dependencia.DependenciaId);

            if (dependenciaExistente != null)
            {
                dependenciaExistente.NombreDependencia = dependencia.NombreDependencia;
                dependenciaExistente.Encargado = dependencia.Encargado;

                _context.Dependencias.Update(dependenciaExistente);
                await _context.SaveChangesAsync();
            }
        }

        public async Task EliminarDependenciaAsync(int id)
        {
            var dependencia = await _context.Dependencias.FindAsync(id);
            if (dependencia != null)
            {
                _context.Dependencias.Remove(dependencia);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Bombero?>> ObtenerBomberosDeDependenciaAsync(int dependenciaId)
        {
            var dependencia = await _context.Dependencias
                .Include(d => d.Bomberos) // Aquí Bomberos es de tipo List<Bombero_Dependencia>
                .FirstOrDefaultAsync(d => d.DependenciaId == dependenciaId);

            return dependencia?.Bomberos
                .Select(bd => bd.Bombero) // Proyecta los bomberos reales
                .ToList() ?? new List<Bombero?>();
        }

        public async Task AgregarBomberoADependenciaAsync(int dependenciaId, int bomberoId)
        {
            // Verificar si la dependencia existe
            var dependencia = await _context.Dependencias
                .FirstOrDefaultAsync(d => d.DependenciaId == dependenciaId);
            if (dependencia == null)
                throw new Exception("Dependencia no encontrada");

            // Verificar si el bombero existe
            var bombero = await _context.Bomberos
                .FirstOrDefaultAsync(b => b.PersonaId == bomberoId);
            if (bombero == null)
                throw new Exception("Bombero no encontrado");

            // Verificar si ya existe la relación
            var existeRelacion = await _context.Set<Bombero_Dependencia>()
                .AnyAsync(bd => bd.DependenciaId == dependenciaId && bd.PersonaId == bomberoId);

            if (existeRelacion)
                throw new Exception("El bombero ya pertenece a la dependencia");

            // Crear la relación y guardarla
            var relacion = new Bombero_Dependencia
            {
                DependenciaId = dependenciaId,
                PersonaId = bomberoId
            };

            _context.Set<Bombero_Dependencia>().Add(relacion);
            await _context.SaveChangesAsync();
        }

        public async Task QuitarBomberoDeDependenciaAsync(int dependenciaId, int bomberoId)
        {
            // Verificar si la relación existe
            var relacion = await _context.Set<Bombero_Dependencia>()
                .FirstOrDefaultAsync(bd => bd.DependenciaId == dependenciaId && bd.PersonaId == bomberoId);

            if (relacion == null)
                throw new Exception("La relación entre el bombero y la dependencia no existe");

            // Eliminar la relación
            _context.Set<Bombero_Dependencia>().Remove(relacion);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ComprobarBomberoEnDependenciaAsync(int dependenciaId, int bomberoId)
        {
            return await _context.Set<Bombero_Dependencia>()
                .AnyAsync(bd => bd.DependenciaId == dependenciaId && bd.PersonaId == bomberoId);
        }
    }
}
