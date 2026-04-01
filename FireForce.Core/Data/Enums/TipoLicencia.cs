using System.ComponentModel.DataAnnotations;
namespace Vista.Data.Enums
{
    /// <summary>
    /// Tipos de licencia disponibles.
    /// </summary>
    public enum TipoLicencia
    {
        /// <summary>
        /// Ausencia por razones laborales.
        /// </summary>
        [Display(Name = "Razones laborales")]
        RazonesLaborales = 1,

        /// <summary>
        /// Ausencia por razones familiares.
        /// </summary>
        [Display(Name = "Razones familiares")]
        RazonesFamiliares = 2,

        /// <summary>
        /// Ausencia por razones de estudio.
        /// </summary>
        [Display(Name = "Razones de estudio")]
        RazonesDeEstudio = 3,

        /// <summary>
        /// Ausencia por razones de salud.
        /// </summary>
        [Display(Name = "Razones de salud")]
        RazonesDeSalud = 4,

        /// <summary>
        /// Ausencia fuera de la ciudad.
        /// </summary>
        [Display(Name = "Ausencia de la ciudad")]
        AusenciaDeLaCiudad = 5
    }
}