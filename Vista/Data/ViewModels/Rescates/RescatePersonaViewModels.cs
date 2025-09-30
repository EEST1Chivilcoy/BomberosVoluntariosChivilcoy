using System.ComponentModel.DataAnnotations;
using Vista.Data.Enums;

namespace Vista.Data.ViewModels.Rescates
{
    public class RescatePersonaViewModels : RescateViewModels
    {
        public TipoLugarRescatePersona TipoRescatePersona { get; set; }
    }
}
