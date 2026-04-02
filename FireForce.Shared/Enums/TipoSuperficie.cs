using System.ComponentModel.DataAnnotations;
namespace FireForce.Shared.Enums
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
