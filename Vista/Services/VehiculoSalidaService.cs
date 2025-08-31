using Microsoft.EntityFrameworkCore;
using Vista.Data.Models.Imagenes;
using Vista.Data;
using Vista.Data.Enums;
using Vista.Data.Models.Vehiculos.Flota;

namespace Vista.Services
{
    public interface IVehiculoSalidaService
    {
        // Métodos existentes
        Task<List<VehiculoSalida>> ObtenerTodosLosVehiculosSalidasAsync();
        Task<VehiculoSalida?> ObtenerVehiculoSalidaPorNumeroMovilAsync(string numeromovil);
        Task<List<VehiculoSalida>> ObtenerVehiculosSalidasPorEstadoAsync(TipoEstadoMovil estado);
        Task<VehiculoSalida?> ObtenerVehiculoSalidaPorIdAsync(int id);
        Task<VehiculoSalida?> ObtenerVehiculoSalidaSinRelacionesPorIdAsync(int id);
        Task CambiarEstadoAsync(int id, TipoEstadoMovil estado);
        Task<VehiculoSalida> AgregarVehiculoSalidaAsync(VehiculoSalida vehiculo, Imagen? imagen = null);
    }

    public class VehiculoSalidaService : IVehiculoSalidaService
    {
        private readonly BomberosDbContext _context;
        private readonly IBomberoService _bomberosService;
        private readonly IImagenService _imagenService;

        public VehiculoSalidaService(BomberosDbContext context, IBomberoService bomberosService, IImagenService imagenService)
        {
            _context = context;
            _bomberosService = bomberosService;
            _imagenService = imagenService;
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

        public async Task<VehiculoSalida?> ObtenerVehiculoSalidaSinRelacionesPorIdAsync(int id)
        {
            return await _context.VehiculoSalidas
                .FirstOrDefaultAsync(v => v.VehiculoId == id);
        }   

        public async Task<VehiculoSalida?> ObtenerVehiculoSalidaPorNumeroMovilAsync(string numeromovil)
        {
            return await _context.VehiculoSalidas
                .Include(v => v.Encargado)
                .Include(v => v.Imagen)
                .AsNoTracking()
                .FirstOrDefaultAsync(v => v.NumeroMovil == numeromovil);
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

        public async Task<VehiculoSalida> AgregarVehiculoSalidaAsync(VehiculoSalida vehiculo, Imagen? imagen = null)
        {
            if (vehiculo?.Encargado is not null)
            {
                var encargado = await _bomberosService.ObtenerBomberoPorIdAsync(vehiculo.Encargado.PersonaId);

                if (encargado is not null)
                {
                    vehiculo.Encargado = encargado;
                    encargado.VehiculosEncargado ??= new List<VehiculoSalida>();
                    encargado.VehiculosEncargado.Add(vehiculo);
                }
            }

            switch (vehiculo)
            {
                case Movil movil:
                    _context.Moviles.Add(movil);
                    break;
                case Embarcacion embarcacion:
                    _context.Embarcacion.Add(embarcacion);
                    break;
                default:
                    throw new InvalidOperationException("Tipo de vehículo no soportado.");
            }

            await _context.SaveChangesAsync(); // Guardamos primero el vehículo para obtener su ID

            // Si hay imagen, la vinculamos y la guardamos
            if (imagen is Imagen_VehiculoSalida imagenVehiculo)
            {
                imagenVehiculo.VehiculoId = vehiculo.VehiculoId; // Asignamos el ID recién generado
                await _imagenService.GuardarImagenAsync(imagenVehiculo);
            }

            return vehiculo;
        }
    }
}
