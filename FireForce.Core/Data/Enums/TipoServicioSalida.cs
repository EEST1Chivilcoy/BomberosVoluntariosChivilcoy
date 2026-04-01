namespace Vista.Data.Enums;

using System.ComponentModel.DataAnnotations;

public enum TipoServicioSalida
{
    [Display(Name = "Asistencia Obligatoria")] // ---> Capacitaciones
    AsistenciaObligatoria,

    [Display(Name = "Asistencia Accidental")] // ---> Salidas con alarma
    AsistenciaAccidental,

    [Display(Name = "Servicio Guardia")] // ---> Salidas sin alarma
    ServicioGuardia,

    //[Display(Name = "Dedicación Orden Interno")] // ---> Firmas (No va aca)
    //DedicacionOrdenInterno,

    //[Display(Name = "Servicio Especial")] // ---> Puntaje del Jefe (No va aca) (Pasa a ser parte de un ABM aparte)
    //Especial
}
