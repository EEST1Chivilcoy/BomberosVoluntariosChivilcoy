using System.ComponentModel.DataAnnotations;
namespace Vista.Data.Enums
{
    public enum TipoNivelCapacitacion
    {
        [Display(Name = "Cuartel")]
        Cuartel,
        [Display(Name = "Federativa")]
        Federativa,
        [Display(Name = "Nacional")]
        Nacional,
        [Display(Name = "Internacional")]
        Internacional,
        [Display(Name = "Regional")]
        Regional,
        [Display(Name = "Otro")]
        Otro,
    }
}
