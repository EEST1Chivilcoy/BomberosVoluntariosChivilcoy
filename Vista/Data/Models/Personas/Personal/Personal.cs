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
        /// Grupo sanguíneo de la persona.
        /// </summary>
        public TipoGrupoSanguineo GrupoSanguineo { get; set; }

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
        /// Observaciones adicionales sobre la persona.
        /// </summary>
        public string? Observaciones { get; set; }

        /// <summary>
        /// Profesión de la persona. Longitud máxima de 255 caracteres.
        /// </summary>
        [StringLength(255)]
        public string? Profesion { get; set; }

        /// <summary>
        /// Nivel de estudios alcanzado por la persona. Longitud máxima de 255 caracteres.
        /// </summary>
        [StringLength(255)]
        public string? NivelEstudios { get; set; }

        /// <summary>
        /// Número de IOMA (cobertura de salud). Longitud máxima de 255 caracteres.
        /// </summary>
        [StringLength(255)]
        public string? NumeroIoma { get; set; }

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
