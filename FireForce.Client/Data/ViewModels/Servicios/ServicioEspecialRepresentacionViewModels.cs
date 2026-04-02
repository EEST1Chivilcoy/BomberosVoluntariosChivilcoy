using System.ComponentModel.DataAnnotations;
using FireForce.Core.Data.Enums;

namespace FireForce.Client.Data.ViewModels.Servicios
{
    public class ServicioEspecialRepresentacionViewModels : ServicioEspecialViewModel
    {
        [Required]
        public TipoServicioRepresentaciones TipoServicioRepresentacion { get; set; }

        [StringLength(255)]
        public string? OtroRepresentacion { get; set; }

        [Required]
        public TipoOrganizacionBeneficiada TipoOrganizacion { get; set; }

        [StringLength(255)]
        public string? OtraOrganizacion { get; set; }
    }
}
