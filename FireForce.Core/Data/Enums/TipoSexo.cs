using System.ComponentModel.DataAnnotations;

namespace FireForce.Core.Data.Enums
{
    public enum TipoSexo
    {
        [Display(Name = "Hombre")]
        Hombre,

        [Display(Name = "Mujer")]
        Mujer,

        [Display(Name = "Otro")]
        Otro
    }
}
