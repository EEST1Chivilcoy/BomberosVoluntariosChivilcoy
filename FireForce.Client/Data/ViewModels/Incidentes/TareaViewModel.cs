using FireForce.Shared.Enums.Firmas;

namespace FireForce.Client.Data.ViewModels.Incidentes
{
    /// <summary>
    /// Tarea derivada de un incidente. Las subtareas cuelgan de <see cref="Subtareas"/>, de modo
    /// que la cadena de derivaciones se puede recorrer tal como se dibujó en la pizarra.
    /// </summary>
    public class TareaViewModel
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = string.Empty;

        public string Descripcion { get; set; } = string.Empty;

        public DateTime Fecha { get; set; }

        public EstadoTarea Estado { get; set; }

        public string DependenciaNombre { get; set; } = string.Empty;

        /// <summary>
        /// Encargado del departamento destino. Es quien figura como autor cuando desde esta tarea
        /// se vuelve a derivar o se cambia el estado.
        /// </summary>
        public int EncargadoId { get; set; }

        public string EncargadoNombre { get; set; } = string.Empty;

        public int? TareaPadreId { get; set; }

        public List<TareaViewModel> Subtareas { get; set; } = new();
    }
}
