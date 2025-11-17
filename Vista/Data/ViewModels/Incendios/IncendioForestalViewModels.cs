using System.ComponentModel.DataAnnotations;
using Vista.Data.Enums;

namespace Vista.Data.ViewModels.Incendios
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
