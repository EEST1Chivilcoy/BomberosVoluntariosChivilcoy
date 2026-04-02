using System.ComponentModel.DataAnnotations;

namespace FireForce.Shared.Enums
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
