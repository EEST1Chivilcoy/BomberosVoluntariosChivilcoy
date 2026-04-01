namespace Vista.Data.Enums;
using System.ComponentModel.DataAnnotations;

public enum EstadoBombero
{
    [Display(Name = "Cuerpo Activo")]
    CuerpoActivo,

    [Display(Name = "Reserva")]
    Reserva,

    [Display(Name = "Baja")]
    Baja,

    [Display(Name = "Disponibilidad")]
    Disponibilidad,

    [Display(Name = "Baja por Fallecimiento")]
    BajaPorFallecimiento
}
