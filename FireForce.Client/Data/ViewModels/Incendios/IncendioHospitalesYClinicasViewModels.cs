using System.ComponentModel.DataAnnotations;
using FireForce.Core.Data.Enums;

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
