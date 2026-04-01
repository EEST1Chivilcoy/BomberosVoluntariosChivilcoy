using System.ComponentModel.DataAnnotations;
namespace Vista.Data.Enums
{
    public enum TipoLugarRescateAnimal
    {
        [Display(Name = "Casa")]
        Casa,
        [Display(Name = "Edificio")]
        Edificio,
        [Display(Name = "Centro Comercial")]
        CentroComercial,
        [Display(Name = "Rio")]
        Rio,
        [Display(Name = "Bosque")]
        Bosque,
        [Display(Name = "Otro")]
        Otro
    }
}
