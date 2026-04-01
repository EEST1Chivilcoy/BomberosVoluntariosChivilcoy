using System.ComponentModel.DataAnnotations;
namespace FireForce.Core.Data.Enums
{
    public enum TipoRubro 
    {
        [Display(Name = "No Asignado")]
        NoAsignado,
        [Display(Name = "Taller")]
        Taller,
        [Display(Name = "Personal")]
        Personal,
        [Display(Name = "Seguridad")]
        Seguridad
    }
}
