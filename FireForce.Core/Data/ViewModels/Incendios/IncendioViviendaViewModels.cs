using System.ComponentModel.DataAnnotations;
using FireForce.Core.Data.Enums;

namespace FireForce.Core.Data.ViewModels.Incendios
{
    public class IncendioViviendaViewModels : IncendioViewModels
    {
        [Required]
        public TipoIncendioVivienda TipoLugar { get; set; }
    }
}
