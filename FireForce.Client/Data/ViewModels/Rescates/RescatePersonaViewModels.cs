using FireForce.Shared.Enums;

namespace FireForce.Client.Data.ViewModels.Rescates
{
    public class RescatePersonaViewModels : RescateViewModels
    {
        /// <summary>
        /// Tipo de lugar del rescate de persona.
        /// </summary>
        public TipoLugarRescatePersona? TipoRescatePersona { get; set; }
    }
}
