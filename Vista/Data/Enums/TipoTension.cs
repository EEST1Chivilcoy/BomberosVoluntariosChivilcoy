using System.ComponentModel.DataAnnotations;

namespace Vista.Data.Enums
{
    public enum TipoTension
    {
        [Display(Name = "12V")]
        DoceVolts,
        [Display(Name = "24V")]
        VeintiCuatroVolts
    }
}
