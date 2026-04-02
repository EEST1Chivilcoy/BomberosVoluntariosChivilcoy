using System.ComponentModel.DataAnnotations;
using FireForce.Core.Data.Enums;

namespace FireForce.Core.Data.ViewModels.Rescates
{
    public class RescateAnimaViewModels : RescateViewModels
    {
        /// <summary>
        /// Tipo de lugar del rescate animal.
        /// </summary>
        public TipoLugarRescateAnimal? TipoRescateAnimal { get; set; }
    }
}
