using System.ComponentModel.DataAnnotations;
using FireForce.Data.Models.Personas;
using FireForce.Data.Models.Personas.Personal;
using FireForce.Shared.Enums.Discriminadores;
using FireForce.Shared.Enums.Firmas;

namespace FireForce.Data.Models.Otros.Firmas
{
    /// <summary>
    /// Incidente reportado sobre un móvil o sobre una dependencia.
    /// Es la raíz de la jerarquía TPH discriminada por <see cref="TipoDeIncidente"/>.
    /// </summary>
    public abstract class IncidenteBase
    {
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Discriminador: indica si el incidente es sobre un móvil o sobre una dependencia.
        /// </summary>
        public TipoDeIncidente Tipo { get; set; }

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
        /// Solo un <see cref="Bombero"/> puede crear categorías distintas de
        /// <see cref="CategoriaIncidente.Novedad"/>; cualquier otra <see cref="Persona"/> solo
        /// puede reportar novedades.
        /// </summary>
        [Required]
        public CategoriaIncidente Categoria { get; set; }

        // --- Estado del incidente ---

        public EstadoIncidente Estado { get; set; } = EstadoIncidente.Pendiente;

        /// <summary>
        /// Cantidad de firmas que el encargado otorgó al aprobar el incidente.
        /// Las recibe el <see cref="Solicitante"/>, siempre que sea un <see cref="Bombero"/> y la
        /// categoría no sea <see cref="CategoriaIncidente.Novedad"/>. Cada firma equivale a 1 punto.
        /// </summary>
        public int CantidadFirmas { get; set; }

        // --- Solicitante del incidente ---

        /// <summary>
        /// Identificador del solicitante.
        /// </summary>
        [Required]
        public int SolicitanteId { get; set; }

        /// <summary>
        /// Quien reporta el incidente. Puede ser cualquier persona, no solo un bombero.
        /// </summary>
        /// <remarks>
        /// La obligatoriedad la impone <see cref="SolicitanteId"/>, que no admite null. No lleva
        /// <c>[Required]</c> para que se pueda validar la entidad con DataAnnotations teniendo solo
        /// la clave foránea cargada, sin materializar la navegación.
        /// </remarks>
        public Persona Solicitante { get; set; } = null!;

        // --- Encargado del incidente ---

        /// <summary>
        /// Identificador del encargado del incidente.
        /// </summary>
        [Required]
        public int EncargadoId { get; set; }

        /// <summary>
        /// Encargado del móvil o de la dependencia sobre la que se reporta.
        /// Se asigna al crear el incidente y <b>no cambia</b>: derivar a otro departamento se hace
        /// creando una <see cref="Tarea"/>, no reasignando el incidente.
        /// </summary>
        /// <remarks>Ver la nota de <see cref="Solicitante"/> sobre por qué no lleva <c>[Required]</c>.</remarks>
        public Bombero Encargado { get; set; } = null!;

        // --- Colecciones ---

        /// <summary>
        /// Tareas derivadas del incidente. Cada derivación a otro departamento crea una tarea.
        /// </summary>
        public List<Tarea> Tareas { get; set; } = new();

        /// <summary>
        /// Bitácora de auditoría del ciclo de vida del incidente.
        /// </summary>
        public List<MovimientoIncidente> Movimientos { get; set; } = new();
    }
}
