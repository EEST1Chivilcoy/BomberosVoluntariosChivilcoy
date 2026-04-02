using System.ComponentModel.DataAnnotations;

namespace FireForce.Shared.Enums
{
    public enum TipoTension
    {
        [Display(Name = "12V")]
        DoceVolts,
        [Display(Name = "24V")]
        VeintiCuatroVolts
    }
}
