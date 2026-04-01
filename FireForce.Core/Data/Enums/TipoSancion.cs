using System.ComponentModel.DataAnnotations;
namespace Vista.Data.Enums
{
    public enum TipoSancion
    {
        [Display(Name = "Sanción")]
        Sancion,
        Apercebimiento,
        Baja
    }
}
