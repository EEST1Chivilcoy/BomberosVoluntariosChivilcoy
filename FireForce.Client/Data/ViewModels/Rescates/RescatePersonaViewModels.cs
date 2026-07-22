using FireForce.Shared.Enums;
using System.ComponentModel.DataAnnotations;

namespace FireForce.Client.Data.ViewModels.Rescates
{
    public class RescatePersonaViewModels : RescateViewModels
    {
        /// <summary>
        /// Tipo de lugar del rescate de persona.
        /// </summary>
        [Required]
        public TipoLugarRescatePersona? TipoRescatePersona { get; set; }
    }
}
