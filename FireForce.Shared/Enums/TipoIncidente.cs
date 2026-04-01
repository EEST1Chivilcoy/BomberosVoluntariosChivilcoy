using System.ComponentModel.DataAnnotations;
namespace FireForce.Core.Data.Enums
{
    public enum TipoIncidente
    {

        [Display(Name = "Gestion")]
        Gestion,    
        [Display(Name = "Limpieza")]
        Limpieza,
        [Display(Name = "Novedad")]
        Novedad,    
        [Display(Name = "Mantenimiento")] 
        Mantenimiento
    }
}

