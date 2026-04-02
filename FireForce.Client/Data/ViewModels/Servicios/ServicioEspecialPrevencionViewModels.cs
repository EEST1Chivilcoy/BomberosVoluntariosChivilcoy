using System.ComponentModel.DataAnnotations;
using FireForce.Core.Data.Enums;

namespace FireForce.Client.Data.ViewModels.Servicios
{
    public class ServicioEspecialPrevencionViewModels : ServicioEspecialViewModel
    {
        /// <summary>
        /// Tipo de organización beneficiada por el servicio de prevención.
        /// </summary>
        [Required]
        public TipoOrganizacionBeneficiada? TipoOrganizacion { get; set; }

        /// <summary>
        /// Tipo de servicio de prevención realizado.
        /// </summary>
        [Required]
        public TipoServicioPrevencion? TipoPrevencion { get; set; }
    }
}
