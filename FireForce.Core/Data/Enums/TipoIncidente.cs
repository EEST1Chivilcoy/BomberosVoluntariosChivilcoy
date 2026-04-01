using System.ComponentModel.DataAnnotations;
namespace Vista.Data.Enums
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

