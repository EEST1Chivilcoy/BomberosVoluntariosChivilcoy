using Microsoft.EntityFrameworkCore;
using Vista.Data;
using Vista.Data.Enums;
using Vista.Data.Models.Vehiculos.Flota;

namespace Vista.Services
{
    public interface IVehiculoSalidaService
    {
        Task<List<VehiculoSalida>> ObtenerTodosLosVehiculosSalidasAsync();
        Task<List<VehiculoSalida>> ObtenerVehiculosSalidasPorEstadoAsync(TipoEstadoMovil estado);
        Task<VehiculoSalida?> ObtenerVehiculoSalidaPorIdAsync(int id);
        Task CambiarEstadoAsync(int id, TipoEstadoMovil estado);

    }
    public class VehiculoSalidaService : IVehiculoSalidaService
    {
        private readonly BomberosDbContext _context;
        public VehiculoSalidaService(BomberosDbContext context)
        {
            _context = context;
        }

        public async Task<List<VehiculoSalida>> ObtenerTodosLosVehiculosSalidasAsync()
        {
            return await _context.VehiculoSalidas.AsNoTracking().
                ToListAsync();
        }

        public async Task<List<VehiculoSalida>> ObtenerVehiculosSalidasPorEstadoAsync(TipoEstadoMovil estado)
        {
            return await _context.VehiculoSalidas
                .Where(v => v.Estado == estado)
                .Include(v => v.Encargado)
                .Include(v => v.Imagen)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<VehiculoSalida?> ObtenerVehiculoSalidaPorIdAsync(int id)
        {
            return await _context.VehiculoSalidas
                .Include(v => v.Encargado)
                .Include(v => v.Imagen)
                .AsNoTracking()
                .FirstOrDefaultAsync(v => v.VehiculoId == id);
        }

        public async Task CambiarEstadoAsync(int id, TipoEstadoMovil estado)
        {
            VehiculoSalida? vehiculo = await ObtenerVehiculoSalidaPorIdAsync(id);

            if (vehiculo != null)
            {
                vehiculo.Estado = estado;
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException($"Vehículo con ID {id} no encontrado.");
            }
        }
    }
}
