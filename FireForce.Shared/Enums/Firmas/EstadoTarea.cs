using System.ComponentModel.DataAnnotations;

namespace FireForce.Shared.Enums.Firmas
{
    /// <summary>
    /// Enumeración que representa los posibles estados de una tarea.
    /// </summary>
    public enum EstadoTarea
    {
        Pendiente = 0,
        [Display(Name = "En proceso")]
        EnProceso = 1,
        Solucionado = 2,
        [Display(Name = "Sin solución")]
        SinSolucion = 3,
    }
}
