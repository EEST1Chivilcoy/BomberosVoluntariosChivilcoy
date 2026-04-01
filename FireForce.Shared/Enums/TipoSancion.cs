using System.ComponentModel.DataAnnotations;
namespace FireForce.Core.Data.Enums
{
    public enum TipoSancion
    {
        [Display(Name = "Sanción")]
        Sancion,
        Apercebimiento,
        Baja
    }
}
