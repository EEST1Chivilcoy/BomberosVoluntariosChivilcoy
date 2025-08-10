using Microsoft.EntityFrameworkCore;
using System;
using Vista.Data;
using Vista.Data.Models.Personas.Personal.Componentes;
using Vista.Data.Enums;

namespace Vista.Services
{
    public interface ILicenciaService
    {
        Task<List<Licencia>> ObtenerTodasLasLicencias();
        Task<Licencia?> ObtenerLicenciaPorId(int licenciaId);
        Task AgregarLicencia(Licencia licencia);
        Task CambiarEstadoLicencia (int licenciaId, TipoEstadoLicencia nuevoEstado);
    }

    public class LicenciaService : ILicenciaService
    {
        private readonly BomberosDbContext _context;
        private DateTime? _ultimaLimpieza = null; // campo privado

        public LicenciaService(BomberosDbContext context)
        {
            _context = context;
        }

        public async Task<List<Licencia>> ObtenerTodasLasLicencias()
        {
            try
            {
                // Verificar si ya se limpió hoy
                if (_ultimaLimpieza == null || _ultimaLimpieza.Value.Date < DateTime.Today)
                {
                    await LimpiarLicenciasPendientes();
                    _ultimaLimpieza = DateTime.Today;
                }

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

        public async Task CambiarEstadoLicencia(int licenciaId, TipoEstadoLicencia nuevoEstado)
        {
            var licencia = await _context.Licencias.FindAsync(licenciaId);

            if (licencia == null)
            {
                throw new ArgumentException("Licencia no encontrada.", nameof(licenciaId));
            }

            licencia.EstadoLicencia = nuevoEstado;
            _context.Licencias.Update(licencia);
            await _context.SaveChangesAsync();
        }

        // Método privado para limpiar licencias pendientes (no expuesto en la interfaz)
        public async Task LimpiarLicenciasPendientes()
        {
            var licenciasPendientes = await _context.Licencias
                .Where(l => l.EstadoLicencia == TipoEstadoLicencia.Pendiente && l.Desde < DateTime.Today)
                .ToListAsync();

            foreach (var licencia in licenciasPendientes)
            {
                licencia.EstadoLicencia = TipoEstadoLicencia.Rechazada;
                _context.Licencias.Update(licencia);
            }

            await _context.SaveChangesAsync();
        }
    }
}
