using System.ComponentModel.DataAnnotations;
using Vista.Data.Models.Personas.Personal;

namespace Vista.Data.Models.Personas.Personal.Componentes
{
    /// <summary>
    /// Representa los datos de contacto de una persona.
    /// </summary>
    public class Contacto
    {
        /// <summary>
        /// Identificador único del contacto.
        /// </summary>
        public int ContactoId { get; set; }

        /// <summary>
        /// Número de teléfono celular del contacto. Longitud máxima de 255 caracteres.
        /// </summary>
        [StringLength(255, ErrorMessage = "El número de celular no puede superar los 255 caracteres.")]
        [Phone(ErrorMessage = "El número de celular no tiene un formato válido.")]
        public string? TelefonoCel { get; set; }

        /// <summary>
        /// Número de teléfono laboral del contacto. Longitud máxima de 255 caracteres.
        /// </summary>
        [StringLength(255, ErrorMessage = "El número laboral no puede superar los 255 caracteres.")]
        [Phone(ErrorMessage = "El número laboral no tiene un formato válido.")]
        public string? TelefonoLaboral { get; set; }

        /// <summary>
        /// Número de teléfono fijo del contacto. Longitud máxima de 255 caracteres.
        /// </summary>
        [StringLength(255, ErrorMessage = "El número fijo no puede superar los 255 caracteres.")]
        [Phone(ErrorMessage = "El número fijo no tiene un formato válido.")]
        public string? TelefonoFijo { get; set; }

        /// <summary>
        /// Dirección de correo electrónico del contacto. Longitud máxima de 255 caracteres.
        /// </summary>
        [StringLength(255, ErrorMessage = "El email no puede superar los 255 caracteres.")]
        [EmailAddress(ErrorMessage = "El email no tiene un formato válido.")]
        public string? Email { get; set; }

        /// <summary>
        /// Identificador de la persona asociada a este contacto.
        /// </summary>
        public int PersonalId { get; set; }

        /// <summary>
        /// Relación con la entidad `Personal`, que representa a la persona asociada a este contacto.
        /// </summary>
        public Personal Persona { get; set; } = null!;
    }
}
