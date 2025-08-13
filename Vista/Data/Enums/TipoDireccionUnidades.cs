using System.ComponentModel.DataAnnotations;

namespace Vista.Data.Enums
{
    public enum TipoDireccionUnidades
    {
        [Display(Name = "Mecanica")]
        Mecanica,
        [Display(Name = "Hidraulica")]
        Hidraulica,
        [Display(Name = "Electrica")]
        Electrica
    }
}
