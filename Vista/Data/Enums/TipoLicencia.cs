using System.ComponentModel.DataAnnotations;
namespace Vista.Data.Enums
{
    public enum TipoLicencia
    {
        [Display(Name = "Razones personales")]
        RazonesPersonales,

        [Display(Name = "Razones familiares")]
        RazonesFamiliares,

        [Display(Name = "Razones de estudio")]
        RazonesDeEstudio,

        [Display(Name = "Razones de salud")]
        RazonesDeSalud,

        [Display(Name = "Ausencia de la ciudad")]
        AusenciaDeLaCiudad
    }
}