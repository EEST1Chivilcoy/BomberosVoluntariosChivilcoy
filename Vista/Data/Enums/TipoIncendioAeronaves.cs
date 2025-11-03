using System.ComponentModel.DataAnnotations;
namespace Vista.Data.Enums
{
    public enum TipoIncendioAeronaves
    {
        [Display(Name = "Avión")]
        Avion,
        [Display(Name = "Helicóptero")]
        Helicoptero,
        [Display(Name = "Dron")]
        Dron,
        [Display(Name = "Otro")]
        Otro
    }
}