using System.ComponentModel.DataAnnotations;
namespace FireForce.Shared.Enums
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
