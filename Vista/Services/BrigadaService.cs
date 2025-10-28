using Microsoft.EntityFrameworkCore;
using System;
using Vista.Data;
using Vista.Data.Models.Grupos.Brigadas;
using Vista.Data.Models.Personas.Personal;

namespace Vista.Services
{
    public interface IBrigadaService
    {
        Task<List<Brigada>> ObtenerTodasLasBrigadasAsync();
        Task<Brigada?> ObtenerBrigadaPorIdAsync(int id);
        Task<List<Bombero?>> ObtenerBomberosDeBrigadaAsync(int brigadaId);
        Task AgregarBrigadaAsync(Brigada brigada);
        Task EditarBrigadaAsync(Brigada brigada);
        Task EliminarBrigadaAsync(int id);
        Task AgregarBomberoABrigadaAsync(int brigadaId, int bomberoId);
        Task QuitarBomberoDeBrigadaAsync(int brigadaId, int bomberoId);
    }

    public class BrigadaService : IBrigadaService
    {
        private readonly BomberosDbContext _context;

        public BrigadaService(BomberosDbContext context)
        {
            _context = context;
        }

        public async Task<List<Brigada>> ObtenerTodasLasBrigadasAsync()
        {
            return await _context.Brigadas
                .Include(b => b.Encargado)
                .ToListAsync();
        }

        public async Task<Brigada?> ObtenerBrigadaPorIdAsync(int id)
        {
            return await _context.Brigadas.FindAsync(id);
        }

        public async Task AgregarBrigadaAsync(Brigada brigada)
        {
            _context.Brigadas.Add(brigada);
            await _context.SaveChangesAsync();
        }

        public async Task EditarBrigadaAsync(Brigada brigada)
        {
            var brigadaExistente = await _context.Brigadas.FindAsync(brigada.BrigadaId);

            if (brigadaExistente != null)
            {
                brigadaExistente.NombreBrigada = brigada.NombreBrigada;
                brigadaExistente.Encargado = brigada.Encargado;

                _context.Brigadas.Update(brigadaExistente);
                await _context.SaveChangesAsync();
            }
        }

        public async Task EliminarBrigadaAsync(int id)
        {
            var brigada = await _context.Brigadas.FindAsync(id);
            if (brigada != null)
            {
                _context.Brigadas.Remove(brigada);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Bombero?>> ObtenerBomberosDeBrigadaAsync(int brigadaId)
        {
            var brigada = await _context.Brigadas
                .Include(b => b.Bomberos) // Aquí Bomberos es de tipo List<Bombero_Brigada>
                .ThenInclude(bd => bd.Bombero) // Incluye los detalles del Bombero
                .FirstOrDefaultAsync(b => b.BrigadaId == brigadaId);

            return brigada?.Bomberos
                .Select(bd => bd.Bombero) // Proyecta los bomberos reales
                .ToList() ?? new List<Bombero?>();
        }

        public async Task AgregarBomberoABrigadaAsync(int brigadaId, int bomberoId)
        {
            // Verificar si la brigada existe
            var brigada = await _context.Brigadas
                .FirstOrDefaultAsync(d => d.BrigadaId == brigadaId);

            if (brigada == null)
                throw new Exception("Brigada no encontrada");

            // Verificar si el bombero existe
            var bombero = await _context.Bomberos
                .FirstOrDefaultAsync(b => b.PersonaId == bomberoId);

            if (bombero == null)
                throw new Exception("Bombero no encontrado");

            // Verificar si ya existe la relación
            var existeRelacion = await _context.Set<Bombero_Brigada>()
                .AnyAsync(bd => bd.BrigadaId == brigadaId && bd.PersonaId == bomberoId);

            if (existeRelacion)
                throw new Exception("El bombero ya pertenece a la Brigada");

            // Crear la relación y guardarla
            var relacion = new Bombero_Brigada
            {
                BrigadaId = brigadaId,
                PersonaId = bomberoId
            };

            _context.Set<Bombero_Brigada>().Add(relacion);
            await _context.SaveChangesAsync();
        }

        public async Task QuitarBomberoDeBrigadaAsync(int brigadaId, int bomberoId)
        {
            // Verificar si la relación existe
            var relacion = await _context.Set<Bombero_Brigada>()
                .FirstOrDefaultAsync(bd => bd.BrigadaId == brigadaId && bd.PersonaId == bomberoId);

            if (relacion == null)
                throw new Exception("La relación entre el bombero y la brigada no existe");

            // Eliminar la relación
            _context.Set<Bombero_Brigada>().Remove(relacion);
            await _context.SaveChangesAsync();
        }
    }
}