using System.ComponentModel.DataAnnotations;
using Vista.Data.Enums;
using Vista.Data.Models.Grupos.Brigadas;

namespace Vista.Data.ViewModels.Personal
{
    public class BomberoViweModel : PersonalViewModel
    {
        /// <summary>
        /// Estado del bombero.
        /// Normalmente se establece en 'CuerpoActivo' al crear un nuevo bombero.
        /// </summary>
        [Required(ErrorMessage = "El estado del bombero es obligatorio.")]
        public EstadoBombero? Estado { get; set; } = EstadoBombero.CuerpoActivo;

        /// <summary>
        /// Número de legajo del bombero.
        /// Es un campo obligatorio y debe ser único para cada bombero.
        /// Son a asignados por la administración de los bomberos.
        /// </summary>
        [Required(ErrorMessage = "El número de legajo del bombero es obligatorio.")]
        [Range(1, int.MaxValue, ErrorMessage = "El número de legajo debe ser mayor que cero.")]
        public int? NumeroLegajo { get; set; }

        /// <summary>
        /// Escalafón jerárquico del bombero.
        /// </summary>
        [Required(ErrorMessage = "El grado jerárquico es obligatorio.")]
        public EscalafonJerarquico? Grado { get; set; }

        /// <summary>
        /// Dotación asignada al bombero.
        /// </summary>
        [Required(ErrorMessage = "La dotación es obligatoria.")]
        public TipoDotaciones? Dotacion { get; set; }

        /// <summary>
        /// Altura del bombero en centímetros.
        /// </summary>
        public int? Altura { get; set; }

        /// <summary>
        /// Peso del bombero en kilogramos.
        /// </summary>
        public int? Peso { get; set; }

        /// <summary>
        /// Si el bombero es chofer.
        /// </summary>
        public bool EsChofer { get; set; }

        /// <summary>
        /// Vencimiento del registro del bombero.
        /// Esto si es chofer.
        /// </summary>
        public DateTime? VencimientoRegistro { get; set; }

        /// <summary>
        /// Observaciones adicionales sobre el bombero.
        /// </summary>

        [StringLength(255)]
        public string? Observaciones { get; set; }

        /// <summary>
        /// Profesión del bombero.
        /// </summary>

        [StringLength(255)]
        public string? Profesion { get; set; }

        /// <summary>
        /// Nivel de estudios del bombero. (Pasar a Enum)
        /// </summary>

        [StringLength(255)]
        public string? NivelEstudios { get; set; }

        /// <summary>
        /// Número de IOMA del bombero.
        /// </summary>
        [StringLength(255)]
        public string? NumeroIoma { get; set; }

        /// <summary>
        /// Nombres de las brigadas a las que pertenece el bombero.
        /// </summary>
        [StringLength(255)]
        public string? NombresBrigada { get; set; }
    }
}