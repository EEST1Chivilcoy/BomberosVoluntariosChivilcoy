using System.ComponentModel.DataAnnotations;
using Vista.Data.Enums;

namespace Vista.Data.ViewModels.Incendios
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
