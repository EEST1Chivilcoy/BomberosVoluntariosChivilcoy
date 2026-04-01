using System.ComponentModel.DataAnnotations;
namespace FireForce.Core.Data.Enums
{
    public enum TipoSuperficie
    {
        [Display(Name = "Kilómetro")]
        Km,
        [Display(Name = "Hectáreas")]
        Hectareas,
        [Display(Name = "Metros")]
        Metros
    }
}
