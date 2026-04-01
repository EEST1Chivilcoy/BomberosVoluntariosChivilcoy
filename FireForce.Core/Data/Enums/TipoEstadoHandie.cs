using System.ComponentModel.DataAnnotations;
namespace FireForce.Core.Data.Enums
{
    public enum TipoEstadoHandie
    {
        Stock,
        Activo,
        Baja,
        [Display(Name = "Reparación")]
        Reparacion
    }
}
