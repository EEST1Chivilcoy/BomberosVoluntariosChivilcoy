using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Office2016.Drawing.Command;
using Microsoft.EntityFrameworkCore;
using Vista.Data;
using Vista.Data.Enums;
using Vista.Data.Models.Imagenes;
using Vista.Data.Models.Vehiculos.Flota;

namespace Vista.Services
{
    public interface IVehiculoSalidaService
    {
        // Métodos existentes
        Task<List<VehiculoSalida>> ObtenerTodosLosVehiculosSalidasAsync();
        Task<VehiculoSalida?> ObtenerVehiculoSalidaPorNumeroMovilAsync(string numeromovil);
        Task<List<VehiculoSalida>> ObtenerVehiculosSalidasPorEstadoAsync(TipoEstadoMovil estado);
        Task<VehiculoSalida?> ObtenerVehiculoSalidaPorIdAsync(int id, bool asnotracking = true);
        Task<VehiculoSalida?> ObtenerVehiculoSalidaSinRelacionesPorIdAsync(int id);
        Task CambiarEstadoAsync(int id, TipoEstadoMovil estado);
        Task<VehiculoSalida> AgregarVehiculoSalidaAsync(VehiculoSalida vehiculo, Imagen? imagen = null);
        Task<bool> EditarVehiculo(VehiculoSalida vehiculo, Imagen? imagen = null);
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

        public async Task<VehiculoSalida?> ObtenerVehiculoSalidaPorIdAsync(int id, bool asnotracking = true)
        {
            if (asnotracking)
            {
                return await _context.VehiculoSalidas
                    .Include(v => v.Encargado)
                    .Include(v => v.Imagen)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(v => v.VehiculoId == id);
            }
            else
            {
                return await _context.VehiculoSalidas
                .Include(v => v.Encargado)
                .Include(v => v.Imagen)
                .FirstOrDefaultAsync(v => v.VehiculoId == id);
            }
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
            VehiculoSalida? vehiculo = await ObtenerVehiculoSalidaPorIdAsync(id, false);

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
            if (imagen is null)
            {
                if (imagen is Imagen_VehiculoSalida imagenVehiculo)
                {
                    imagenVehiculo.VehiculoId = vehiculo.VehiculoId; // Asignamos el ID recién generado
                    await _imagenService.GuardarImagenAsync(imagenVehiculo);
                }
                else
                {
                    throw new InvalidOperationException("Tipo de imagen no soportado para vehículos.");
                }
            }

            return vehiculo;
        }

        public async Task<bool> EditarVehiculo(VehiculoSalida vehiculo, Imagen? imagen = null)
        {
            if (vehiculo is null) return false;

            var existente = await ObtenerVehiculoSalidaPorIdAsync(vehiculo.VehiculoId, false)
                ?? throw new KeyNotFoundException($"Vehículo con ID {vehiculo.VehiculoId} no encontrado.");

            bool cambioDeTipo = (existente is Movil && vehiculo is Embarcacion) || (existente is Embarcacion && vehiculo is Movil);

            if (cambioDeTipo)
            {
                await ReemplazarVehiculoPorTipoNuevoAsync(existente, vehiculo, imagen);
            }
            else
            {
                ActualizarPropiedadesEscalares(existente, vehiculo);
                await SetEncargadoAsync(existente, vehiculo);
                await ProcesarImagenAsync(existente, vehiculo.Imagen);
                await _context.SaveChangesAsync();
            }

            return true;
        }

        private async Task SetEncargadoAsync(VehiculoSalida destino, VehiculoSalida origen)
        {
            var id = origen.Encargado?.PersonaId ?? origen.EncargadoId;
            if (id.HasValue)
            {
                var enc = await _bomberosService.ObtenerBomberoPorIdAsync(id.Value);
                destino.Encargado = enc;
                destino.EncargadoId = enc?.PersonaId;
            }
        }

        private void ActualizarPropiedadesEscalares(VehiculoSalida destino, VehiculoSalida origen)
        {
            var ignoradas = new[] { nameof(VehiculoSalida.Encargado), nameof(VehiculoSalida.EncargadoId), nameof(VehiculoSalida.Imagen), nameof(VehiculoSalida.ImagenId) };
            foreach (var prop in origen.GetType().GetProperties().Where(p => !ignoradas.Contains(p.Name)))
            {
                var destinoProp = destino.GetType().GetProperty(prop.Name);
                if (destinoProp?.CanWrite == true)
                {
                    var valor = prop.GetValue(origen);
                    if (valor is not null) destinoProp.SetValue(destino, valor);
                }
            }
        }

        private async Task ProcesarImagenAsync(VehiculoSalida vehiculo, Imagen? imagen)
        {
            if (imagen is Imagen_VehiculoSalida imgVehiculo)
            {
                imgVehiculo.VehiculoId = vehiculo.VehiculoId;

                if (vehiculo.ImagenId.HasValue)
                {
                    imgVehiculo.ImagenId = vehiculo.ImagenId.Value;
                    await _imagenService.EditarImagenAsync(imgVehiculo);
                }
                else
                {
                    await _imagenService.GuardarImagenAsync(imgVehiculo);
                    vehiculo.ImagenId = imgVehiculo.ImagenId;
                }
            }
        }

        private async Task ReemplazarVehiculoPorTipoNuevoAsync(VehiculoSalida existente, VehiculoSalida nuevo, Imagen? imagen)
        {
            int? imagenIdExistente = existente.ImagenId;

            _context.Set<VehiculoSalida>().Remove(existente);
            await _context.SaveChangesAsync();

            await SetEncargadoAsync(nuevo, nuevo);

            if (imagen is null && imagenIdExistente.HasValue)
            {
                var img = await _context.ImagenesVehiculo.FirstOrDefaultAsync(i => i.ImagenId == imagenIdExistente.Value);
                if (img is not null)
                {
                    nuevo.Imagen = img;
                    nuevo.ImagenId = img.ImagenId;
                }
            }

            switch (nuevo)
            {
                case Movil m: _context.Moviles.Add(m); break;
                case Embarcacion e: _context.Embarcacion.Add(e); break;
                default: throw new InvalidOperationException("Tipo de vehículo no soportado.");
            }

            await _context.SaveChangesAsync();

            if (imagen is not null)
            {
                if (imagenIdExistente.HasValue)
                {
                    imagen.ImagenId = imagenIdExistente.Value;
                    await _imagenService.EditarImagenAsync(imagen);
                    nuevo.ImagenId = imagen.ImagenId;
                }
                else
                {
                    await ProcesarImagenAsync(nuevo, imagen);
                }

                await _context.SaveChangesAsync();
            }
        }
    }
}
