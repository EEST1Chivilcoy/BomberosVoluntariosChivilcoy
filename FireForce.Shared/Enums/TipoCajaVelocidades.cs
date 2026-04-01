using System.ComponentModel.DataAnnotations;

namespace FireForce.Core.Data.Enums
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
