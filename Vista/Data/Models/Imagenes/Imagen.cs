using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Vista.Data.Enums.Discriminadores;

namespace Vista.Data.Models.Imagenes
{
    public abstract class Imagen
    {
        /// <summary>
        /// Identificador único de la imagen.
        /// </summary>
        public int ImagenId { get; set; }

        /// <summary>
        /// Discriminador para el tipo de imagen (Personal, Vehiculo, etc).
        /// </summary>
        public TipoImagen Tipo { get; set; }

        /// <summary>
        /// Nombre de la imagen. Campo obligatorio con una longitud máxima de 255 caracteres.
        /// </summary>
        [Required(ErrorMessage = "El nombre de la imagen es obligatorio.")]
        [StringLength(255, ErrorMessage = "El nombre de la imagen no puede superar los 255 caracteres.")]
        public string NombreImagen { get; set; } = null!;

        /// <summary>
        /// Datos de la imagen en formato binario.
        /// </summary>
        [Required(ErrorMessage = "Los datos binarios de la imagen son obligatorios.")]
        public byte[] DatosImagen { get; set; } = null!;

        /// <summary>
        /// Tipo o formato de la imagen (por ejemplo, JPEG, PNG). 
        /// Campo obligatorio con una longitud máxima de 255 caracteres.
        /// </summary>
        [Required(ErrorMessage = "El tipo de imagen es obligatorio.")]
        [StringLength(255, ErrorMessage = "El tipo de imagen no puede superar los 255 caracteres.")]
        public string TipoImagen { get; set; } = null!;

        /// <summary>
        /// Base64 de la imagen. Este campo no se almacena en la base de datos.
        /// </summary>
        [NotMapped]
        public string Base64Imagen
        {
            get => Convert.ToBase64String(DatosImagen);
            set => DatosImagen = Convert.FromBase64String(value);
        }
    }
}
