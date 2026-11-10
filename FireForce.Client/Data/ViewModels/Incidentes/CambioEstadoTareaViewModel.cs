using System.ComponentModel.DataAnnotations;
using FireForce.Shared.Enums.Firmas;

namespace FireForce.Client.Data.ViewModels.Incidentes
{
    /// <summary>
    /// Datos del formulario con el que el encargado de una tarea le cambia el estado.
    /// La observación queda en la bitácora del incidente, así cada paso de la cadena
    /// (En proceso → Solucionado, etc.) deja registro de qué se hizo.
    /// </summary>
    public class CambioEstadoTareaViewModel
    {
        public EstadoTarea Estado { get; set; }

        [StringLength(500, ErrorMessage = "La descripción no puede superar los 500 caracteres.")]
        public string? Observaciones { get; set; }
    }
}
