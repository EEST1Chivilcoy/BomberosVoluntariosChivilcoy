namespace Vista.Data.Enums;
using System.ComponentModel.DataAnnotations;

public enum TipoServiciosEspeciales
{
    [Display(Name = "Capacitación")]
    Capacitacion,
    [Display(Name = "Especiales")]
    Especiales, // TODO: Agregar
    [Display(Name = "Prevención")]
    Prevencion,
    [Display(Name = "Falsa Alarma")]
    FalsaAlarma,
    [Display(Name = "Representación")]
    Representacion,
    [Display(Name = "Suministro de Agua")]
    SuministroDeAgua, 
    [Display(Name = "Retirado de Obito")]
    RetiradoDeObito, // TODO: Agregar
    [Display(Name = "Colaboración con Fuerzas de Seguridad")]
    ColaboracionConFuerzasDeSeguridad, // TODO: Agregar
    [Display(Name = "Colocación de Driza")]
    ColaboracionDeDriza,
    [Display(Name = "Otro")]
    Otro // TODO: Agregar
}
