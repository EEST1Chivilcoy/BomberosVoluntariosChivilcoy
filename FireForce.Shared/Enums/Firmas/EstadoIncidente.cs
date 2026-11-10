using System.ComponentModel.DataAnnotations;

namespace FireForce.Shared.Enums.Firmas
{
    /// <summary>
    /// Enumeración que representa los posibles estados de un incidente.
    /// </summary>
    public enum EstadoIncidente
    {
        [Display(Name = "Pendiente")]
        Pendiente = 0,

        [Display(Name = "En proceso")]
        EnProceso = 1,

        [Display(Name = "Aprobado")]
        Aprobado = 2,

        // En el cuartel lo llaman "rechazado"; el nombre del valor se mantiene por compatibilidad.
        [Display(Name = "Rechazado")]
        Desaprobado = 3,
    }
}
