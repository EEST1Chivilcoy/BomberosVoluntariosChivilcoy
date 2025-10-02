using Microsoft.EntityFrameworkCore;
using Vista.Data;
using Vista.Data.Models.Imagenes;
using Vista.DTOs;

namespace Vista.Services
{
    public interface IImagenService
    {
        Task<ImagenResultado?> ObtenerImagenAsync(int id);
        Task<bool> GuardarImagenAsync(Imagen imagen);
        Task EditarImagenAsync(Imagen imagen);
        Task EliminarImagenAsync(int? id);
    }

    public class ImagenService : IImagenService
    {
        private readonly BomberosDbContext _context;

        public ImagenService(BomberosDbContext context)
        {
            _context = context;
        }

        public async Task<ImagenResultado?> ObtenerImagenAsync(int id)
        {
            var imagen = await _context.Imagen.FindAsync(id);
            if (imagen == null)
                return null;

            return new ImagenResultado
            {
                Datos = imagen.DatosImagen,
                Formato = imagen.TipoImagen
            };
        }

        public async Task<bool> GuardarImagenAsync(Imagen imagen)
        {
            try
            {
                // Manejar las relaciones según el tipo de imagen
                switch (imagen)
                {
                    case Imagen_VehiculoSalida imagen_VehiculoSalida:
                        if (imagen_VehiculoSalida.VehiculoId != 0)
                        {
                            var vehiculo = await _context.VehiculoSalidas.FindAsync(imagen_VehiculoSalida.VehiculoId);

                            if (vehiculo != null)
                            {
                                imagen_VehiculoSalida.Vehiculo = vehiculo;
                            }
                        }
                        break;
                    case Imagen_Personal imagen_Personal:
                        if (imagen_Personal.PersonalId != 0)
                        {
                            var personal = await _context.Bomberos.FindAsync(imagen_Personal.PersonalId);

                            if (personal != null)
                            {
                                imagen_Personal.Personal = personal;
                            }
                        }
                        break;
                    default:
                        throw new InvalidOperationException("Tipo de imagen no soportado para guardar.");
                }

                _context.Imagen.Add(imagen);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new InvalidOperationException("Error inesperado al guardar una Imagen.");
            }
        }

        public async Task EditarImagenAsync(Imagen imagen)
        {
            var imagenExistente = await _context.Imagen.FindAsync(imagen.ImagenId);

            if (imagenExistente != null)
            {
                imagenExistente.NombreImagen = imagen.NombreImagen;
                imagenExistente.DatosImagen = imagen.DatosImagen;
                imagenExistente.TipoImagen = imagen.TipoImagen;
                _context.Imagen.Update(imagenExistente);
                await _context.SaveChangesAsync();
            }
        }

        public async Task EliminarImagenAsync(int? id)
        {
            if (id == null || id <= 0)
            {
                // Podés loguear esto como parte de un ritual de validación fallida
                Console.WriteLine($"[❌] ID inválido para eliminación: {id}");
            }

            try
            {
                var imagen = await _context.Imagen.FindAsync(id);

                if (imagen == null)
                {
                    Console.WriteLine($"[⚠️] No se encontró imagen con ID: {id}");
                }

                _context.Imagen.Remove(imagen);
                await _context.SaveChangesAsync();

                Console.WriteLine($"[✅] Imagen con ID {id} eliminada correctamente.");
            }
            catch (Exception ex)
            {
                // Ideal para logging con timestamp, emoji y separador de lore
                Console.WriteLine($"[🔥] Error al eliminar imagen con ID {id}: {ex.Message}");
            }
        }
    }
}
