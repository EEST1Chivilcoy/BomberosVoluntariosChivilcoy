using System.ComponentModel.DataAnnotations;

namespace FireForce.Shared.Enums.Firmas
{
    /// <summary>
    /// Enumeración que representa los posibles estados de un incidente.
    /// </summary>
    public enum EstadoIncidente
    {
        Pendiente = 0,
        [Display(Name = "En proceso")]
        EnProceso = 1,
        Aprobado = 2,
        Desaprobado = 3,
    }
}
