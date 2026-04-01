using Vista.Data.Enums;
using System.ComponentModel.DataAnnotations;
using Vista.Data.Enums.Discriminadores;

namespace Vista.Data.Models.Personas
{
    /// <summary>
    /// Clase base abstracta que representa a una persona.  
    /// Proporciona una estructura común para clases derivadas que necesiten atributos y comportamientos relacionados con una persona.
    /// </summary>
    public abstract class Persona
    {
        /// <summary>
        /// Identificador único de la persona.
        /// </summary>
        public int PersonaId { get; set; }

        /// <summary>
        /// Tipo de persona, que puede ser Bombero, Comisión Directiva.
        /// </summary>
        public TipoPersonal Tipo { get; set; }

        /// <summary>
        /// Sexo de la persona.
        /// </summary>
        [Required(ErrorMessage = "El sexo de la persona es obligatorio.")]
        public TipoSexo Sexo { get; set; }

        /// <summary>
        /// Nombre de la persona. Campo obligatorio con una longitud máxima de 255 caracteres.
        /// </summary>
        [Required(ErrorMessage = "El nombre de la persona es obligatorio.")]
        [StringLength(255, ErrorMessage = "El nombre no puede superar los 255 caracteres.")]
        public string Nombre { get; set; } = null!;

        /// <summary>
        /// Apellido de la persona. Campo obligatorio con una longitud máxima de 255 caracteres.
        /// </summary>
        [Required(ErrorMessage = "El apellido de la persona es obligatorio.")]
        [StringLength(255, ErrorMessage = "El apellido no puede superar los 255 caracteres.")]
        public string Apellido { get; set; } = null!;

        /// <summary>
        /// Número de documento de identidad de la persona. Campo obligatorio.
        /// </summary>
        [Required(ErrorMessage = "El número de documento es obligatorio.")]
        [Range(1000000, 99999999, ErrorMessage = "El número de documento debe estar entre 1.000.000 y 99.999.999 para documentos argentinos.")]
        public int Documento { get; set; }

        /// <summary>
        /// Residencia habitual de la persona. Longitud máxima de 255 caracteres.
        /// </summary>
        [StringLength(255)]
        public string? Residencia { get; set; }

        /// <summary>
        /// Dirección donde reside la persona. Longitud máxima de 255 caracteres.
        /// </summary>
        [StringLength(255)]
        public string? Direccion { get; set; }

        /// <summary>
        /// Edad de la persona. Calculada o asignada manualmente.
        /// </summary>
        public virtual int Edad { get; set; }
    }
}
