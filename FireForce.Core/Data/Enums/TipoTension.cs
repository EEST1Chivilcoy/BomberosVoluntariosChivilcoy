using System.ComponentModel.DataAnnotations;

namespace FireForce.Core.Data.Enums
{
    public enum TipoTension
    {
        [Display(Name = "12V")]
        DoceVolts,
        [Display(Name = "24V")]
        VeintiCuatroVolts
    }
}
