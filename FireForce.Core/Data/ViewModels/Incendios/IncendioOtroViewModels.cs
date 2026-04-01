using System.ComponentModel.DataAnnotations;
using Vista.Data.Enums;
namespace Vista.Data.ViewModels.Incendios
{
    public class IncendioOtroViewModels : IncendioViewModels
    {
        [Required]
        public TipoIncendioOtro OtroIncendio{ get; set; }
    }
}