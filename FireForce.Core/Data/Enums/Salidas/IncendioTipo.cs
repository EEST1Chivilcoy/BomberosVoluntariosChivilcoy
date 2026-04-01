using System.ComponentModel.DataAnnotations;

namespace Vista.Data.Enums.Salidas
{
    /// <summary>
    /// Tipos de Incendios
    /// </summary>
    public enum IncendioTipo
    {
        /// <summary>
        /// Incendio en Vivienda
        /// </summary>
        [Display(Name = "Vivienda")]
        Vivienda,

        /// <summary>
        /// Incendio en Comercio
        /// </summary>
        [Display(Name = "Comercio")]
        Comercio,

        /// <summary>
        /// Incendio en Establecimiento Educativo
        /// </summary>
        [Display(Name = "Establecimiento Educativo")]
        EstablecimientoEducativo,

        /// <summary>
        /// Incendio en Establecimiento Público
        /// </summary>
        [Display(Name = "Establecimiento Público")]
        EstablecimientoPublico,

        /// <summary>
        /// Incendio de Hospitales y Clínicas
        /// </summary>
        [Display(Name = "Hospitales y Clínicas")]
        HospitalesYClinicas,

        /// <summary>
        /// Incendio Industrial
        /// </summary>
        [Display(Name = "Industria")]
        Industria,

        /// <summary>
        /// Incendio Forestal
        /// </summary>
        [Display(Name = "Forestal")]
        Forestal,

        /// <summary>
        /// Incendio de Aeronaves
        /// </summary>
        [Display(Name = "Aeronaves")]
        Aeronaves,

        /// <summary>
        /// Otro tipo de incendios
        /// </summary>
        [Display(Name = "Otro")]
        Otro
    }
}
