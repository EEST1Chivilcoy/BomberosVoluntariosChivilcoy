using System.ComponentModel.DataAnnotations;
namespace Vista.Data.Enums
{
    public enum TipoLlenadoPlanilla
    {
        [Display(Name = "Lleno")]
        Lleno,
        [Display(Name = "No Lleno")]
        NoLleno,
        [Display(Name = "Encargado")]
        Encargado
    }
}
