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

        /// <summary>
        /// ID de la cuenta de EntraID
        /// </summary>
        public Guid EntraID { get; set; }

        // --- Propiedades básicas del personal --- (Información Personal)

        /// <summary>
        /// Nombre del personal. Longitud máxima de 255 caracteres. Es obligatorio.
        /// </summary>
        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(255, ErrorMessage = "El nombre no puede superar los 255 caracteres.")]
        public string? Nombre { get; set; }

        /// <summary>
        /// Apellido del personal. Longitud máxima de 255 caracteres. Es obligatorio.
        /// </summary>
        [Required(ErrorMessage = "El apellido es obligatorio.")]
        [StringLength(255, ErrorMessage = "El apellido no puede superar los 255 caracteres.")]
        public string? Apellido { get; set; }

        /// <summary>
        /// UPN (User Principal Name) del personal. Longitud máxima de 255 caracteres. Es obligatorio.
        /// </summary>
        [Required(ErrorMessage = "El UPN es obligatorio.")]
        [StringLength(255, ErrorMessage = "El UPN no puede superar los 255 caracteres.")]
        public string? UPN { get; set; }

        /// <summary>
        /// URL de la imagen de perfil del personal. Longitud máxima de 255 caracteres. Es opcional.
        /// </summary>
        public string? UrlImagen { get; set; }

        /// <summary>
        /// Documento del personal. Es obligatorio.
        /// </summary>
        [Required(ErrorMessage = "El número de documento es obligatorio.")]
        [Range(1000000, 99999999, ErrorMessage = "El número de documento debe estar entre 1.000.000 y 99.999.999 para documentos argentinos.")]
        public int? Documento { get; set; }

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
        [Required(ErrorMessage = "El sexo es obligatorio.")]
        public TipoSexo? Sexo { get; set; }

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
        [StringLength(255, ErrorMessage = "El número de celular no puede superar los 255 caracteres.")]
        [Phone(ErrorMessage = "El número de celular no tiene un formato válido.")]
        public string? TelefonoCel { get; set; }

        /// <summary>
        /// Número de teléfono laboral del personal. Longitud máxima de 255 caracteres. Es opcional.
        /// </summary>
        [StringLength(255, ErrorMessage = "El número laboral no puede superar los 255 caracteres.")]
        [Phone(ErrorMessage = "El número laboral no tiene un formato válido.")]
        public string? TelefonoLaboral { get; set; }

        /// <summary>
        /// Email del personal. Longitud máxima de 255 caracteres. Es opcional.
        /// </summary>
        [StringLength(255, ErrorMessage = "El email no puede superar los 255 caracteres.")]
        [EmailAddress(ErrorMessage = "El email no tiene un formato válido.")]
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
