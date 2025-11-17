using System.ComponentModel.DataAnnotations;
using Vista.Data.Enums;
using Vista.Data.Models.Grupos.Dependencias;
using Vista.Data.Models.Personas.Personal;

namespace Vista.Data.Models.Otros.Firmas
{
    public abstract class IncidenteBase
    {
        [Key]
        public int Id { get; set; }

        // Tipo de incidente (Movil o Dependencia)

        /// <summary>
        /// Fecha en que se registró el incidente.
        /// </summary>
        [Required]
        public DateTime Fecha { get; set; } = DateTime.Now;

        /// <summary>
        /// Descripción del incidente.
        /// </summary>
        [Required]
        public string Descripcion { get; set; } = string.Empty;

        /// <summary>
        /// Categoría del incidente.
        /// </summary>
        [Required]
        public CategoriaIncidente Categoria { get; set; }

        // --- Estado del incidente ---

        public EstadoIncidente Estado { get; set; } = EstadoIncidente.Pendiente;

        public DateTime? FechaEstado { get; set; } = DateTime.Now;

        public string? ObservacionesEstado { get; set; }


        // --- Responsable del incidente ---

        /// <summary>
        /// Identificador del bombero responsable.
        /// </summary>
        [Required]
        public int AtendidoPorId { get; set; }

        /// <summary>
        /// Bombero responsable.
        /// </summary>
        [Required]
        public Bombero AtendidoPor { get; set; } = null!;

        // --- Encargado del incidente ---

        /// <summary>
        /// Identificador del encargado del incidente.
        /// </summary>
        [Required]
        public int EncargadoId { get; set; }

        /// <summary>
        /// Encargado del incidente.
        /// </summary>
        [Required]
        public Bombero Encargado { get; set; } = null!;

        // --- Datos Adiccionales si es Novedad ---

        public Dependencia? DependenciaANotificar{ get; set; }

        public int? DependenciaANotificarId { get; set; }
    }
}
