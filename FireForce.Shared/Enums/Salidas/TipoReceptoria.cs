using System.ComponentModel.DataAnnotations;

namespace FireForce.Shared.Enums.Salidas
{
    public enum TipoReceptoria
    {
        [Display(Name = "Casero")]
        Casero = 1,

        [Display(Name = "Secretaria")]
        Secretaria = 2,

        [Display(Name = "Bombero")]
        Bombero = 3,

        [Display(Name = "Otros")]
        Otros = 4
    }
}
