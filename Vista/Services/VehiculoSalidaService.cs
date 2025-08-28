using Microsoft.EntityFrameworkCore;
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
        Task CambiarEstadoAsync(int id, TipoEstadoMovil estado);

        // Método principal de edición
        Task<VehiculoSalida> EditarVehiculo(VehiculoSalida vehiculo);
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

        public async Task<VehiculoSalida> EditarVehiculo(VehiculoSalida vehiculo)
        {
            if (vehiculo?.VehiculoId == null)
                throw new ArgumentException("El vehículo y su ID no pueden ser nulos", nameof(vehiculo));

            try
            {
                var vehiculoExistente = await _context.Set<VehiculoSalida>()
                    .Include(v => v.Encargado)
                    .Include(v => v.Imagen)
                    .SingleOrDefaultAsync(e => e.VehiculoId == vehiculo.VehiculoId);

                if (vehiculoExistente == null)
                    throw new InvalidOperationException($"No se encontró el vehículo con ID {vehiculo.VehiculoId}");

                // Verificar si hay cambio de tipo (Embarcacion <-> Movil)
                bool esCambioTipo = EsCambioTipoVehiculo(vehiculoExistente, vehiculo);

                if (esCambioTipo)
                {
                    return await ProcesarCambioTipoVehiculo(vehiculoExistente, vehiculo);
                }
                else
                {
                    return await ActualizarVehiculoExistente(vehiculoExistente, vehiculo);
                }
            }
            catch (DbUpdateException ex)
            {
                // Log más específico
                var mensaje = $"Error de base de datos al editar el vehículo {vehiculo.VehiculoId}: {ex.Message}";
                if (ex.InnerException != null)
                    mensaje += $" | Inner: {ex.InnerException.Message}";

                Console.WriteLine(mensaje);
                // Considera usar un logger en lugar de Console.WriteLine
                throw new InvalidOperationException("Error al actualizar el vehículo en la base de datos", ex);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error inesperado al editar vehículo {vehiculo.VehiculoId}: {ex.Message}");
                throw;
            }
        }

        private static bool EsCambioTipoVehiculo(VehiculoSalida existente, VehiculoSalida nuevo)
        {
            return (existente is Embarcacion && nuevo is Movil) ||
                   (existente is Movil && nuevo is Embarcacion);
        }

        private async Task<VehiculoSalida> ProcesarCambioTipoVehiculo(VehiculoSalida vehiculoExistente, VehiculoSalida vehiculoNuevo)
        {
            // Preservar imagen si el nuevo vehículo no tiene una
            int? imagenIdAPreservar = null;
            if (vehiculoNuevo.Imagen == null && vehiculoExistente.Imagen != null)
            {
                imagenIdAPreservar = vehiculoExistente.ImagenId;
            }

            // Remover el vehículo existente
            _context.Set<VehiculoSalida>().Remove(vehiculoExistente);
            await _context.SaveChangesAsync();

            // Configurar encargado
            await ConfigurarEncargado(vehiculoNuevo);

            // Restaurar imagen si es necesario
            if (imagenIdAPreservar.HasValue && imagenIdAPreservar.Value != 0)
            {
                await RestaurarImagen(vehiculoNuevo, imagenIdAPreservar.Value);
            }

            // Agregar el nuevo vehículo según su tipo
            AgregarVehiculoSegunTipo(vehiculoNuevo);

            await _context.SaveChangesAsync();
            return vehiculoNuevo;
        }

        private async Task<VehiculoSalida> ActualizarVehiculoExistente(VehiculoSalida vehiculoExistente, VehiculoSalida vehiculoNuevo)
        {
            // Actualizar propiedades usando reflexión de manera más segura
            ActualizarPropiedades(vehiculoExistente, vehiculoNuevo);

            // Configurar encargado
            if (vehiculoNuevo.Encargado != null)
            {
                var encargado = await _context.Bomberos
                    .SingleOrDefaultAsync(b => b.PersonaId == vehiculoNuevo.Encargado.PersonaId);

                if (encargado != null)
                {
                    vehiculoExistente.Encargado = encargado;
                    vehiculoExistente.EncargadoId = encargado.PersonaId;
                }
            }

            await _context.SaveChangesAsync();
            return vehiculoExistente;
        }

        private static void ActualizarPropiedades(VehiculoSalida destino, VehiculoSalida origen)
        {
            var propiedadesExcluidas = new HashSet<string>
    {
        "Encargado", "EncargadoId", "SeguroId", "VehiculoId"
    };

            var propiedades = origen.GetType().GetProperties()
                .Where(p => p.CanRead && !propiedadesExcluidas.Contains(p.Name));

            foreach (var propiedad in propiedades)
            {
                var valor = propiedad.GetValue(origen);
                var propiedadDestino = destino.GetType().GetProperty(propiedad.Name);

                if (propiedadDestino != null && propiedadDestino.CanWrite && valor != null)
                {
                    propiedadDestino.SetValue(destino, valor);
                }
            }
        }

        private async Task ConfigurarEncargado(VehiculoSalida vehiculo)
        {
            if (vehiculo.Encargado == null) return;

            var encargado = await _context.Bomberos
                .Include(b => b.VehiculosEncargado)
                .SingleOrDefaultAsync(b => b.PersonaId == vehiculo.Encargado.PersonaId);

            if (encargado != null)
            {
                vehiculo.Encargado = encargado;
                vehiculo.EncargadoId = encargado.PersonaId;

                encargado.VehiculosEncargado ??= new List<VehiculoSalida>();
                if (!encargado.VehiculosEncargado.Contains(vehiculo))
                {
                    encargado.VehiculosEncargado.Add(vehiculo);
                }
            }
        }

        private async Task RestaurarImagen(VehiculoSalida vehiculo, int imagenId)
        {
            var imagen = await _context.ImagenesVehiculo
                .SingleOrDefaultAsync(i => i.ImagenId == imagenId);

            if (imagen != null)
            {
                vehiculo.Imagen = imagen;
                vehiculo.ImagenId = imagen.ImagenId;
            }
        }

        private void AgregarVehiculoSegunTipo(VehiculoSalida vehiculo)
        {
            switch (vehiculo)
            {
                case Movil movil:
                    _context.Moviles.Add(movil);
                    break;
                case Embarcacion embarcacion:
                    _context.Embarcacion.Add(embarcacion);
                    break;
                default:
                    throw new InvalidOperationException($"Tipo de vehículo no soportado: {vehiculo.GetType().Name}");
            }
        }
    }
}
