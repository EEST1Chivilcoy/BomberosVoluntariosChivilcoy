using System.ComponentModel.DataAnnotations;
using Vista.Data.Enums;

namespace Vista.Data.ViewModels.Personal
{
    /// <summary>
    /// ViewModel que representa la información básica de una persona que es parte del personal de los Bomberos Voluntarios de Chivilcoy.
    /// </summary>
    public class PersonalViewModel
    {
        /// <summary>
        /// Identificador único del personal.
        /// </summary>
        public int Id { get; set; }

        // --- Propiedades básicas del personal --- (Información Personal)

        /// <summary>
        /// Nombre del personal. Longitud máxima de 255 caracteres. Es obligatorio.
        /// </summary>
        [Required, StringLength(255)]
        public string? Nombre { get; set; }

        /// <summary>
        /// Apellido del personal. Longitud máxima de 255 caracteres. Es obligatorio.
        /// </summary>

        [Required, StringLength(255)]
        public string? Apellido { get; set; }

        /// <summary>
        /// URL de la imagen de perfil del personal. Longitud máxima de 255 caracteres. Es opcional.
        /// </summary>
        public string? UrlImagen { get; set; }

        /// <summary>
        /// Documento del personal. Es obligatorio.
        /// </summary>
        [Required]
        public int Documento { get; set; }

        /// <summary>
        /// Fecha de nacimiento del personal. Es opcional.
        /// </summary>
        public DateTime? FechaNacimiento { get; set; }

        /// <summary>
        /// Grupo sanguíneo del personal. Es opcional.
        /// </summary>
        public TipoGrupoSanguineo? GrupoSanguineo { get; set; }

        /// <summary>
        /// Sexo del personal. Es Obligatorio. (M o F)
        /// </summary>
        [Required]
        public TipoSexo Sexo { get; set; }

        /// <summary>
        /// Dirección del personal. Longitud máxima de 255 caracteres. Es opcional.
        /// </summary>
        [StringLength(255)]
        public string? Direccion { get; set; }

        /// <summary>
        /// Lugar de nacimiento del personal. Longitud máxima de 255 caracteres. Es opcional.
        /// </summary>
        [StringLength(255)]
        public string? LugarNacimiento { get; set; }

        // --- Propiedades de la Profesión --- (Información Profesional)

        /// <summary>
        /// Fecha de aceptación del personal. Es opcional.
        /// </summary>
        public DateTime? FechaAceptacion { get; set; }

        // --- Propiedades de Contacto --- (Información de Contacto)

        /// <summary>
        /// Número de teléfono celular del personal. Longitud máxima de 255 caracteres. Es opcional.
        /// </summary>
        [StringLength(255)]
        public string? TelefonoCel { get; set; }

        /// <summary>
        /// Número de teléfono laboral del personal. Longitud máxima de 255 caracteres. Es opcional.
        /// </summary>
        [StringLength(255)]
        public string? TelefonoLaboral { get; set; }

        /// <summary>
        /// Número de teléfono fijo del personal. Longitud máxima de 255 caracteres. Es opcional.
        /// </summary>
        [StringLength(255)]
        public string? TelefonoFijo { get; set; }

        /// <summary>
        /// Email del personal. Longitud máxima de 255 caracteres. Es opcional.
        /// </summary>
        [StringLength(255)]
        public string? Email { get; set; }

        // --- Variables calculadas --- (No asignables directamente) (Solo lectura)

        /// <summary>
        /// Nombre y Apellido concatenados del personal.
        /// </summary>
        public string NombreYApellido
        {
            get { return Nombre + "," + Apellido; }
        }

        /// <summary>
        /// Apellido y Nombre concatenados del personal.
        /// </summary>
        public string ApellidoYNombre
        {
            get { return Apellido + "," + Nombre; }
        }
    }
}
