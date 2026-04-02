using System.ComponentModel.DataAnnotations;
namespace FireForce.Shared.Enums
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
