using System.ComponentModel.DataAnnotations;
using FireForce.Shared.Enums;

namespace FireForce.Client.Data.ViewModels.Incendios
{
    public class IncendioHospitalesYClinicasViewModels : IncendioViewModels
    {
        /// <summary>
        /// Tipo de hospital o clínica donde ocurrió el incendio.
        /// </summary>
        [Required]
        public TipoIncendioHospitalesYClinicas TipoLugar { get; set; }
    }
}
