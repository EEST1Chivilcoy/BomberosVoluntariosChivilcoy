using System.ComponentModel.DataAnnotations;

namespace Vista.Data.Enums.Salidas
{
    /// <summary>
    /// Categoría de Emergencia
    /// </summary>
    public enum CategoriaEmergencia
    {
        /// <summary>
        /// Categoria de Incendio
        /// </summary>
        [Display(Name = "Incendio")]
        Incendio,

        /// <summary>
        /// Categoria de Rescate
        /// </summary>
        [Display(Name = "Rescate")]
        Rescate,

        /// <summary>
        /// Salida de Material Peligroso
        /// </summary>
        [Display(Name = "Material Peligroso")]
        MaterialPeligroso,

        /// <summary>
        /// Salida de Factor Climático
        /// </summary>
        [Display(Name = "Factor Climático")]
        FactorClimatico,

        /// <summary>
        /// Salida de Accidente
        /// </summary>
        [Display(Name = "Accidente")]
        Accidente,

        /// <summary>
        /// Categoria de Servicio Especial
        /// </summary>
        [Display(Name = "Servicio Especial")]
        ServicioEspecial
    }
}
