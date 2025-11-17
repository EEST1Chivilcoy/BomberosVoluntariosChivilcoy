using System.ComponentModel.DataAnnotations;
using Vista.Data.Enums;

namespace Vista.Data.ViewModels.Rescates
{
    public class RescatePersonaViewModels : RescateViewModels
    {
        /// <summary>
        /// Tipo de lugar del rescate de persona.
        /// </summary>
        public TipoLugarRescatePersona? TipoRescatePersona { get; set; }
    }
}
