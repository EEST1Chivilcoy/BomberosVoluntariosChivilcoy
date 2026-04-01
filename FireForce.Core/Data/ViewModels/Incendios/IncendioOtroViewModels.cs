using System.ComponentModel.DataAnnotations;
using FireForce.Core.Data.Enums;
namespace FireForce.Core.Data.ViewModels.Incendios
{
    public class IncendioOtroViewModels : IncendioViewModels
    {
        [Required]
        public TipoIncendioOtro OtroIncendio{ get; set; }
    }
}