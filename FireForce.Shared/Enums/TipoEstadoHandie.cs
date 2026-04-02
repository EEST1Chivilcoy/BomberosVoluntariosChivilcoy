using System.ComponentModel.DataAnnotations;
namespace FireForce.Shared.Enums
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
