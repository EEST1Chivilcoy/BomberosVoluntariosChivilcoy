using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Vista.Data.Enums;
using Vista.Data.Models.Imagenes;
using Vista.Data.Models.Personas;
using Vista.Data.Models.Personas.Personal.Componentes;
using Vista.Data.Models.Vehiculos;

namespace Vista.Data.Models.Personas.Personal
{
    /// <summary>
    /// Clase abstracta que representa a una persona que es parte del personal de la institución.
    /// </summary>
    public abstract class Personal : Persona
    {
        /// <summary>
        /// ID de la cuenta de EntraID
        /// </summary>
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid EntraId { get; set; }

        [Required(ErrorMessage = "El UPN es obligatorio.")]
        [MaxLength(50, ErrorMessage = "El UPN no puede superar los 50 caracteres.")]
        [RegularExpression(@"^[^@\s]+@bomberoschivilcoy\.org\.ar$", ErrorMessage = "El UPN debe tener el dominio @bomberoschivilcoy.org.ar y contenido antes del @.")]
        public string UPN { get; set; } = string.Empty;

        /// <summary>
        /// Grupo sanguíneo de la persona.
        /// </summary>
        public TipoGrupoSanguineo? GrupoSanguineo { get; set; }

        /// <summary>
        /// Fecha de nacimiento de la persona. Es opcional.
        /// </summary>
        public DateTime? FechaNacimiento { get; set; }

        /// <summary>
        /// Edad calculada en base a la fecha de nacimiento.
        /// </summary>
        public override int Edad
        {
            get
            {
                if (FechaNacimiento.HasValue)
                {
                    var today = DateTime.Today;
                    int edad = today.Year - FechaNacimiento.Value.Year;

                    if (FechaNacimiento.Value.Date > today.AddYears(-edad))
                        edad--;

                    return edad;
                }
                return 0;
            }
        }

        /// <summary>
        /// Lugar de nacimiento de la persona. Longitud máxima de 255 caracteres.
        /// </summary>
        [StringLength(255)]
        public string? LugarNacimiento { get; set; }

        /// <summary>
        /// Información de contacto de la persona. Es opcional.
        /// </summary>
        public Contacto? Contacto { get; set; }

        /// <summary>
        /// Relación con la entidad Imagen_Personal, que representa la imagen asociada a este objeto.
        /// </summary>
        public Imagen_Personal? Imagen { get; set; }

        /// <summary>
        /// Lista de vehículos personales asociados a la persona. Inicializada como una lista vacía por defecto.
        /// </summary>
        public List<Vehiculo_Personal> VehiculosPersonales { get; set; } = new();

        /// <summary>
        /// Fecha en la que se realizó la aceptación. Puede ser nula.
        /// </summary>
        public DateTime? FechaAceptacion { get; set; }
    }
}
