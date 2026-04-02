using System.ComponentModel.DataAnnotations;
namespace FireForce.Shared.Enums
{
    public enum TipoSancion
    {
        [Display(Name = "Sanción")]
        Sancion,
        Apercebimiento,
        Baja
    }
}
