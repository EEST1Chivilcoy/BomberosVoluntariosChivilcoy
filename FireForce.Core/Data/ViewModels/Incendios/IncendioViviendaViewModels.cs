using System.ComponentModel.DataAnnotations;
using Vista.Data.Enums;

namespace Vista.Data.ViewModels.Incendios
{
    public class IncendioViviendaViewModels : IncendioViewModels
    {
        [Required]
        public TipoIncendioVivienda TipoLugar { get; set; }
    }
}
