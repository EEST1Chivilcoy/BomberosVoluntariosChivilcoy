using System.ComponentModel.DataAnnotations;
namespace Vista.Data.Enums
{
    public enum TipoMovimiento
    {
        [Display(Name = "Entrada")]
        Entrada,
        [Display(Name = "Salida")]
        Salida,
        [Display(Name = "Baja")]
        Baja,
        [Display(Name = "Entrada-Baja")]
        Entrada_Salida,
    }
}
