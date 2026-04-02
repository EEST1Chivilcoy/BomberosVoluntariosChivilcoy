using System.ComponentModel.DataAnnotations;
using FireForce.Shared.Enums;

namespace FireForce.Client.Data.ViewModels.Incendios
{
    public class IncendioTransitoViewModel
    {
        /// <summary>
        /// Tipo de vehículo de tránsito involucrado en el incendio.
        /// </summary>
        [Required]
        public TipoVehiculoDeTransito? VehiculoDeTransito { get; set; }

        /// <summary>
        /// Otro tipo de vehículo de tránsito, si aplica.
        /// </summary>
        [StringLength(255)]
        public string? OtroVehiculoDeTransito { get; set; }
    }
}
