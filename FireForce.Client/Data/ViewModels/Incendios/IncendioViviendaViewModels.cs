using System.ComponentModel.DataAnnotations;
using FireForce.Shared.Enums;

namespace FireForce.Client.Data.ViewModels.Incendios
{
    public class IncendioViviendaViewModels : IncendioViewModels
    {
        [Required]
        public TipoIncendioVivienda TipoLugar { get; set; }
    }
}
