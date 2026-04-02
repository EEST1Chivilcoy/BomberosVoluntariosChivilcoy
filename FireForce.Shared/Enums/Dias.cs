using System.ComponentModel.DataAnnotations;
namespace FireForce.Shared.Enums
{
    public enum Dias
    {
        [Display(Name = "Domingo")]
        Domingo,
        [Display(Name = "Lunes")]
        Lunes,
        [Display(Name = "Martes")]
        Martes,
        [Display(Name = "Miércoles")]
        Miercoles,
        [Display(Name = "Jueves")]
        Jueves,
        [Display(Name = "Viernes")]
        Viernes,
        [Display(Name = "Sábado")]
        Sabado
    }
}
