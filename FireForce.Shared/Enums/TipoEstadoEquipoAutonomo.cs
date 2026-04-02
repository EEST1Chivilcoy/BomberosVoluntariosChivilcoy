using System.ComponentModel.DataAnnotations;

namespace FireForce.Shared.Enums
{
    public enum TipoEstadoEquipoAutonomo
    {
        [Display(Name = "Stock")]
        Stock,
        [Display(Name = "Activo")]
        Activo,
        [Display(Name = "Baja")]
        Baja,
        [Display(Name = "Reparación")]
        Reparacion,
        [Display(Name = "Prueba Hidráulica")]
        PruebaHidraulica
    }
}
