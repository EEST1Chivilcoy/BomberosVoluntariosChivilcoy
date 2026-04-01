using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FireForce.Core.Data.Models.Personas.Personal;

namespace FireForce.Core.Data.Models.Imagenes
{
    /// <summary>
    /// Representa la imagen de perfil asociada a un miembro del personal.
    /// </summary>
    public class Imagen_Personal : Imagen
    {
        /// <summary>
        /// Relación con la entidad `Personal`, que representa al personal asociado a esta imagen.
        /// </summary>
        public Personal Personal { get; set; } = null!;

        /// <summary>
        /// Identificador único del personal al que está asociada la imagen.
        /// </summary>
        [Required(ErrorMessage = "El campo 'PersonalId' es obligatorio. Debe especificar el identificador único del personal asociado a la imagen.")]
        public int PersonalId { get; set; }
    }
}
