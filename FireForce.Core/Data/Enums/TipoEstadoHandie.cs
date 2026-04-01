using System.ComponentModel.DataAnnotations;
namespace Vista.Data.Enums
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
