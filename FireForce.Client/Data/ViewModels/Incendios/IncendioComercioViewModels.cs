using System.ComponentModel.DataAnnotations;
using FireForce.Shared.Enums;

namespace FireForce.Client.Data.ViewModels.Incendios
{
    public class IncendioComercioViewModels : IncendioViewModels
    {
        /// <summary>
        /// Tipo de comercio donde ocurrió el incendio.
        /// </summary>
        [Required]
        public TipoLugarComercioIncendio TipoLugar { get; set; }
    }
}
