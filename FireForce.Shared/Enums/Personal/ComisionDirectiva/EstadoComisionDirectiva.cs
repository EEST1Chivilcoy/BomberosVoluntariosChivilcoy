using System.ComponentModel.DataAnnotations;

namespace FireForce.Shared.Enums.Personal.ComisionDirectiva
{
    public enum EstadoComisionDirectiva
    {
        [Display(Name = "Activo")]
        Activo,

        [Display(Name = "Baja")]
        Baja,

        [Display(Name = "Baja por Fallecimiento")]
        BajaPorFallecimiento
    }
}
