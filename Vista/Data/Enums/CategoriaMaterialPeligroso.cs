using System.ComponentModel.DataAnnotations;
namespace Vista.Data.Enums
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
