using System.ComponentModel.DataAnnotations;
using FireForce.Core.Data.Enums;

namespace FireForce.Core.Data.ViewModels.Rescates
{
    public class RescatePersonaViewModels : RescateViewModels
    {
        /// <summary>
        /// Tipo de lugar del rescate de persona.
        /// </summary>
        public TipoLugarRescatePersona? TipoRescatePersona { get; set; }
    }
}
