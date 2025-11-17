namespace Vista.Data.Enums;

using System.ComponentModel.DataAnnotations;

public enum TipoServicioSalida
{
    [Display(Name = "Asistencia Obligatoria")]
    AsistenciaObligatoria,
    [Display(Name = "Asistencia Accidental")]
    AsistenciaAccidental,
    [Display(Name = "Servicio Guardia")]
    ServicioGuardia,
    [Display(Name = "Dedicación Orden Interno")]
    DedicacionOrdenInterno,
    [Display(Name = "Servicio Especial")]
    Especial
}
