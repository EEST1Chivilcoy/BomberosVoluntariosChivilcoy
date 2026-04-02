using System.ComponentModel.DataAnnotations;
using FireForce.Shared.Enums;

namespace FireForce.Client.Data.ViewModels.Incendios
{
    public class IncendioOtroViewModels : IncendioViewModels
    {
        [Required]
        public TipoIncendioOtro OtroIncendio{ get; set; }
    }
}