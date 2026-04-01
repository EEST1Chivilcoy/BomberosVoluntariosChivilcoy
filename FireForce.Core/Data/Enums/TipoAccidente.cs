using System.ComponentModel.DataAnnotations;
namespace Vista.Data.Enums
{
    public enum TipoAccidente
    {
        [Display(Name = "Aéreo")]
        Aereo,
        [Display(Name = "Embarcaciones")]
        Embarcaciones,
        [Display(Name = "Tránsito")]
        Transito,
        [Display(Name = "Otro")]
        Otro
    }
}
