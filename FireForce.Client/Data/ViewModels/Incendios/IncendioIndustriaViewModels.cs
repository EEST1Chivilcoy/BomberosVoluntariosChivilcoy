using System.ComponentModel.DataAnnotations;
using FireForce.Shared.Enums;

namespace FireForce.Client.Data.ViewModels.Incendios
{
    public class IncendioIndustriaViewModels : IncendioViewModels
    {
        /// <summary>
        /// Tipo de lugar de incendio industrial.
        /// </summary>
        [Required]
        public TipoIncendioIndustria TipoLugar { get; set; }
    }
}
