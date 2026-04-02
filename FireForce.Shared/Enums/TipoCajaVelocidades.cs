using System.ComponentModel.DataAnnotations;

namespace FireForce.Shared.Enums
{
    public enum TipoCajaVelocidades
    {
        [Display(Name = "Manual")]
        Manual,
        [Display(Name = "Automatica")]
        Automatica,
        [Display(Name = "Semiautomática")]
        SemiAutomatica
    }
}
