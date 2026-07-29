using System.ComponentModel.DataAnnotations;

namespace FireForce.Client.Data.ViewModels.Incidentes
{
    /// <summary>
    /// Datos del formulario con el que un encargado deriva el incidente a otro departamento.
    /// Derivar crea una tarea; si se indica <see cref="TareaPadreId"/>, la nueva queda como
    /// subtarea y así se arma la cadena de derivaciones.
    /// Cada apertura del modal parte de una instancia nueva, así que no necesita el patrón de
    /// edición sobre copia (<c>IEditableViewModel</c>).
    /// </summary>
    public class DerivacionViewModel
    {
        [Range(1, int.MaxValue, ErrorMessage = "Seleccioná el departamento destino.")]
        public int DependenciaDestinoId { get; set; }

        [Required(ErrorMessage = "El nombre de la tarea es obligatorio.")]
        [StringLength(150, MinimumLength = 3,
            ErrorMessage = "El nombre debe tener entre 3 y 150 caracteres.")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "La descripción de la tarea es obligatoria.")]
        [StringLength(1000, MinimumLength = 5,
            ErrorMessage = "La descripción debe tener entre 5 y 1000 caracteres.")]
        public string Descripcion { get; set; } = string.Empty;

        /// <summary>Tarea de la que deriva esta. Null si sale del encargado original.</summary>
        public int? TareaPadreId { get; set; }
    }
}
