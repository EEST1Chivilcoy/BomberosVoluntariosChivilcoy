using FireForce.Shared.Enums;
using System.ComponentModel.DataAnnotations;

namespace FireForce.Client.Data.ViewModels.Rescates
{
    public class RescateAnimaViewModels : RescateViewModels
    {
        /// <summary>
        /// Tipo de lugar del rescate animal.
        /// </summary>
        [Required]
        public TipoLugarRescateAnimal? TipoRescateAnimal { get; set; }
    }
}
