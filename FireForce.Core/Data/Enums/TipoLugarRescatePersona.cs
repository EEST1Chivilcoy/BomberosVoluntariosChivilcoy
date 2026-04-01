using System.ComponentModel.DataAnnotations;
namespace Vista.Data.Enums
{
    public enum TipoLugarRescatePersona
    {
        [Display(Name = "Casa")]
        Casa,
        [Display(Name = "Edificio")]
        Edificio,
        [Display(Name = "Centro Comercial")]
        CentroComercial,
        [Display(Name = "Río")]
        Rio,
        [Display(Name = "Bosque")]
        Bosque,
        [Display(Name = "Pileta")]
        Pileta,
        [Display(Name = "Pozo")]
        Pozo,
        [Display(Name = "Zanja")]
        Zanja,
        [Display(Name = "Otro")]
        Otro
    }
}
