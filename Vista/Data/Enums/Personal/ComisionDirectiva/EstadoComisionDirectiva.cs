using System.ComponentModel.DataAnnotations;

namespace Vista.Data.Enums.Personal.ComisionDirectiva
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
