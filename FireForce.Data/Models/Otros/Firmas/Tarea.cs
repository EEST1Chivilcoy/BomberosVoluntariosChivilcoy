using FireForce.Data.Models.Grupos.Dependencias;
using FireForce.Data.Models.Personas.Personal;
using FireForce.Shared.Enums.Firmas;

namespace FireForce.Data.Models.Otros.Firmas
{
    /// <summary>
    /// Trabajo derivado de un incidente. Crear una tarea es el mecanismo con el que un encargado
    /// deriva el incidente a otro departamento: la tarea indica el departamento destino y su
    /// encargado. Si ese encargado vuelve a derivar, se crea una subtarea (<see cref="Tareas"/>).
    /// </summary>
    public class Tarea
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = null!;

        public string Descripcion { get; set; } = null!;

        public DateTime Fecha { get; set; } = DateTime.Now;

        public EstadoTarea Estado { get; set; } = EstadoTarea.Pendiente;

        // --- Incidente al que pertenece ---

        public int IncidenteId { get; set; }

        public IncidenteBase Incidente { get; set; } = null!;

        // --- Departamento destino ---

        public int DependenciaId { get; set; }

        /// <summary>
        /// Departamento destino de la derivación.
        /// </summary>
        public Dependencia Dependencia { get; set; } = null!;

        public int EncargadoId { get; set; }

        /// <summary>
        /// Encargado del departamento destino.
        /// </summary>
        public Bombero Encargado { get; set; } = null!;

        // --- Encadenamiento de derivaciones ---

        /// <summary>
        /// Tarea de la que deriva esta. Es <c>null</c> cuando la derivación salió directamente del
        /// encargado original del incidente.
        /// </summary>
        public int? TareaPadreId { get; set; }

        public Tarea? TareaPadre { get; set; }

        /// <summary>
        /// Subtareas: derivaciones sucesivas a partir de esta tarea.
        /// </summary>
        public List<Tarea> Tareas { get; set; } = new();
    }
}
