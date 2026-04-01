using System.ComponentModel.DataAnnotations;

namespace Vista.Data.Enums.Salidas
{
    /// <summary>
    /// Tipo de Servicio Especial
    /// </summary>
    public enum ServicioEspecialTipo
    {
        [Display(Name = "Representación")]
        Representacion,

        [Display(Name = "Prevención")]
        Prevencion,

        [Display(Name = "Capacitación")]
        Capacitacion,

        [Display(Name = "Colocación Driza")]
        ColocacionDriza,

        [Display(Name = "Suministro Agua")]
        SuministroAgua,

        [Display(Name = "Falsa Alarma")]
        FalsaAlarma,

        [Display(Name = "Retirado de Óbito")]
        RetiradoDeObito,

        [Display(Name = "Colaboración Fuerzas Seguridad")]
        ColaboracionFuerzasSeguridad
    }
}
