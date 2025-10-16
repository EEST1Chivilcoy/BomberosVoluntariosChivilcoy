using Vista.Data;
using Vista.Data.Models.Grupos.Dependencias.EquiposAutonomos;
using Microsoft.EntityFrameworkCore;
using Vista.Data.Enums;

namespace Vista.Services
{
    public interface IEquipoAutonomoService
    {
        Task CrearEquipoAutonomoAsync(EquipoAutonomo equipo);
        Task<EquipoAutonomo?> ObtenerEquipoAutonomoAsync(int equipoId);
        Task EditarEquipoAutonomoAsync(EquipoAutonomo equipo);
        Task<List<EquipoAutonomo>> ObtenerEquiposAutonomosAsync();
        Task<List<EquipoAutonomo>> ObtenerEquiposAutonomosPorEstadoAsync(TipoEstadoEquipoAutonomo estado);
        Task CambiarEstadoEquipoAutonomoAsync(int equipoId, TipoEstadoEquipoAutonomo nuevoEstado);
    }
    public class EquipoAutonomoService : IEquipoAutonomoService
    {
        private readonly BomberosDbContext _context;

        public EquipoAutonomoService(BomberosDbContext context)
        {
            _context = context;
        }

        public async Task CrearEquipoAutonomoAsync(EquipoAutonomo equipo)
        {
            if (string.IsNullOrWhiteSpace(equipo.NroSerie))
            {
                throw new ArgumentException("El número de serie es obligatorio.");
            }

            _context.EquiposAutonomos.Add(equipo);
            await _context.SaveChangesAsync();
        }

        public async Task EditarEquipoAutonomoAsync(EquipoAutonomo equipo)
        {
            var existingEquipo = await _context.EquiposAutonomos.FindAsync(equipo.EquipoAutonomoId);

            if (existingEquipo == null)
            {
                throw new KeyNotFoundException("Equipo autónomo no encontrado.");
            }

            existingEquipo.NroSerie = equipo.NroSerie;
            existingEquipo.NroTubo = equipo.NroTubo;
            existingEquipo.Marca = equipo.Marca;
            existingEquipo.Modelo = equipo.Modelo;
            existingEquipo.TipoMaterial = equipo.TipoMaterial;

            await _context.SaveChangesAsync();
        }

        public async Task CambiarEstadoEquipoAutonomoAsync(int equipoId, TipoEstadoEquipoAutonomo nuevoEstado)
        {
            var equipo = await _context.EquiposAutonomos.FindAsync(equipoId);

            if (equipo == null)
            {
                throw new KeyNotFoundException("Equipo autónomo no encontrado.");
            }

            equipo.Estado = nuevoEstado;
            _context.EquiposAutonomos.Update(equipo);
            await _context.SaveChangesAsync();
        }

        public async Task<List<EquipoAutonomo>> ObtenerEquiposAutonomosAsync()
        {
            return await _context.EquiposAutonomos.ToListAsync();
        }

        public async Task<List<EquipoAutonomo>> ObtenerEquiposAutonomosPorEstadoAsync(TipoEstadoEquipoAutonomo estado)
        {
            return await _context.EquiposAutonomos
                                 .Where(e => e.Estado == estado)
                                 .ToListAsync();
        }

        public async Task<EquipoAutonomo?> ObtenerEquipoAutonomoAsync(int equipoId)
        {
            return await _context.EquiposAutonomos
                                 .FirstOrDefaultAsync(e => e.EquipoAutonomoId == equipoId);
        }
    }
}
