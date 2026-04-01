using System.ComponentModel.DataAnnotations;
namespace FireForce.Core.Data.Enums
{
    public enum CategoriaMaterialPeligroso
    {
        [Display(Name = "Escape o Fuga")]
        EscapeOFuga,
        [Display(Name = "Derrame")]
        Derrame,
        [Display(Name = "Explosión")]
        Explosion
    }
}
