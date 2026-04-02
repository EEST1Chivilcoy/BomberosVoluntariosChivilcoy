using System.ComponentModel.DataAnnotations;

namespace FireForce.Shared.Enums
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
