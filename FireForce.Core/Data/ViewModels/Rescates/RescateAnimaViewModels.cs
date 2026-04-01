using System.ComponentModel.DataAnnotations;
using Vista.Data.Enums;

namespace Vista.Data.ViewModels.Rescates
{
    public class RescateAnimaViewModels : RescateViewModels
    {
        /// <summary>
        /// Tipo de lugar del rescate animal.
        /// </summary>
        public TipoLugarRescateAnimal? TipoRescateAnimal { get; set; }
    }
}
