using System.ComponentModel.DataAnnotations;

namespace FireForce.Core.Data.Enums.Discriminadores
{
    /// <summary>
    /// Enumeración discriminadora para el tipo de incidente.
    /// </summary>
    public enum TipoDeIncidente
    {
        [Display(Name = "Móvil")]
        Movil = 1,

        [Display(Name = "Dependencia")]
        Dependencia = 2
    }
}
