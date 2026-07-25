using System.ComponentModel.DataAnnotations;

namespace FireForce.Client.Data.ViewModels.Incidentes
{
    /// <summary>
    /// Datos del formulario con el que el encargado aprueba o rechaza un incidente.
    /// </summary>
    public class ResolucionIncidenteViewModel : IEditableViewModel<ResolucionIncidenteViewModel>
    {
        /// <summary>
        /// Firmas a otorgar al solicitante. Cada firma equivale a 1 punto en el sistema de puntajes.
        /// </summary>
        [Range(0, 100, ErrorMessage = "La cantidad de firmas debe estar entre 0 y 100.")]
        public int CantidadFirmas { get; set; }

        [StringLength(500, ErrorMessage = "Las observaciones no pueden superar los 500 caracteres.")]
        public string? Observaciones { get; set; }

        // ✅ Implementación de ICloneable<ResolucionIncidenteViewModel>
        public ResolucionIncidenteViewModel Clonar()
        {
            return new ResolucionIncidenteViewModel
            {
                CantidadFirmas = this.CantidadFirmas,
                Observaciones = this.Observaciones
            };
        }

        // ✅ Implementación de IUpdatable<ResolucionIncidenteViewModel>
        public void ActualizarDesde(ResolucionIncidenteViewModel source)
        {
            this.CantidadFirmas = source.CantidadFirmas;
            this.Observaciones = source.Observaciones;
        }
    }
}
