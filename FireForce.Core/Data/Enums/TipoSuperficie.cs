using System.ComponentModel.DataAnnotations;
namespace Vista.Data.Enums
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
