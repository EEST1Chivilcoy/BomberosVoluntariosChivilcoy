using System.ComponentModel.DataAnnotations;
namespace FireForce.Core.Data.Enums
{
    public enum TipoLugarAmbulancias
    {
        [Display(Name = "Bar")]
        Bar,
        [Display(Name = "Restaurante (Local de Comida)")]
        RestauranteLocalDeComida,
        [Display(Name = "Shopping")]
        Shopping,
        [Display(Name = "Teatro")]
        Teatro,
        [Display(Name = "Cine")]
        Cine,
        [Display(Name = "Otro")]
        Otro
    }
}
