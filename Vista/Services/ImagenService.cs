using AntDesign;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Vista.Data;
using Vista.Data.Models.Imagenes;
using Vista.Data.Models.Personas.Personal;
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

        // Dentro de ImagenService:
        // NOTA: Se ha quitado la lógica de BeginTransaction y Commit/Rollback.
        public async Task<bool> GuardarImagenAsync(Imagen imagen)
        {
            // --- 1. Validación del Modelo (DataAnnotations) ---
            var validationContext = new ValidationContext(imagen, serviceProvider: null, items: null);
            var validationResults = new List<ValidationResult>();
            bool esValido = Validator.TryValidateObject(imagen, validationContext, validationResults, validateAllProperties: true);

            if (!esValido)
            {
                string errores = string.Join(Environment.NewLine, validationResults.Select(r => r.ErrorMessage));
                // Lanza la excepción. Será capturada por el 'catch' del método que lo llamó.
                throw new ValidationException($"El modelo Imagen no es válido: {Environment.NewLine}{errores}");
            }

            // --- 2. Manejar las relaciones ---
            switch (imagen)
            {
                case Imagen_VehiculoSalida imagen_VehiculoSalida:
                    if (imagen_VehiculoSalida.VehiculoId == 0)
                        throw new ArgumentException("El ID del vehículo no puede ser cero.");

                    var vehiculo = await _context.VehiculoSalidas.FindAsync(imagen_VehiculoSalida.VehiculoId);
                    if (vehiculo == null)
                        throw new InvalidOperationException($"No se encontró el vehículo con ID {imagen_VehiculoSalida.VehiculoId}.");

                    imagen_VehiculoSalida.Vehiculo = vehiculo;
                    break;

                case Imagen_Personal imagen_Personal:
                    if (imagen_Personal.PersonalId == 0)
                        throw new ArgumentException("El ID del personal no puede ser cero.");

                    var personal = await _context.Bomberos.FindAsync(imagen_Personal.PersonalId);
                    if (personal == null)
                        throw new InvalidOperationException($"No se encontró el personal con ID {imagen_Personal.PersonalId}.");

                    imagen_Personal.Personal = personal;
                    break;

                default:
                    throw new InvalidOperationException($"Tipo de imagen '{imagen.GetType().Name}' no soportado para guardar.");
            }


            // --- 3. Añadir y Guardar ---
            _context.Imagen.Add(imagen);

            await _context.SaveChangesAsync();
            return true;
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
