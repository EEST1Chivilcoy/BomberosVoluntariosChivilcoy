using System.ComponentModel.DataAnnotations;
using FireForce.Core.Data.Enums;

namespace FireForce.Client.Data.ViewModels.Incendios
{
    public class IncendioForestalViewModels : IncendioViewModels
    {
        /// <summary>
        /// Tipo de lugar forestal donde ocurrió el incendio.
        /// </summary>
        [Required]
        public TipoIncendioForestal TipoLugar { get; set; }
    }
}
