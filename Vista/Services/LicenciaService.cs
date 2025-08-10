using Microsoft.EntityFrameworkCore;
using System;
using Vista.Data;
using Vista.Data.Models.Personas.Personal.Componentes;

namespace Vista.Services
{
    public interface ILicenciaService
    {
        Task<List<Licencia>> ObtenerTodasLasLicencias();
        Task<Licencia?> ObtenerLicenciaPorId(int licenciaId);
        Task AgregarLicencia(Licencia licencia);
    }

    public class LicenciaService : ILicenciaService
    {
        private readonly BomberosDbContext _context;

        public LicenciaService(BomberosDbContext context)
        {
            _context = context;
        }

        public async Task<List<Licencia>> ObtenerTodasLasLicencias()
        {
            try
            {
                return await _context.Licencias
                    .Include(l => l.BomberoAfectado)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }

        public async Task<Licencia?> ObtenerLicenciaPorId(int licenciaId)
        {
            return await _context.Licencias
                .Include(l => l.BomberoAfectado)
                .FirstOrDefaultAsync(l => l.LicenciaId == licenciaId);
        }

        public async Task AgregarLicencia(Licencia licencia)
        {
            if (licencia == null)
            {
                throw new ArgumentNullException(nameof(licencia), "La licencia no puede ser nula.");
            }

            _context.Licencias.Add(licencia);
            await _context.SaveChangesAsync();
        }
    }
}
