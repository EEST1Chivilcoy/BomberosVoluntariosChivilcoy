using System.ComponentModel.DataAnnotations;

namespace Vista.Data.Enums.Socios
{
    /// <summary>
    /// Representa las zonas geográficas o áreas de operación de los socios.
    /// Esto es paramarcar diferentes regiones o sectores en los que los socios pueden estan ubicados.
    /// Son
    /// </summary>
    public enum Zona
    {
        /// <summary>
        /// Representa la Zona 1.
        /// </summary>
        [Display(Name = "Zona 1")]
        Zona1 = 1,

        /// <summary>
        /// Representa la Zona 2.
        /// </summary>
        [Display(Name = "Zona 2")]
        Zona2 = 2,

        /// <summary>
        /// Representa la Zona 3.
        /// </summary>
        [Display(Name = "Zona 3")]
        Zona3 = 3,

        /// <summary>
        /// Representa la Zona 4.
        /// </summary>
        [Display(Name = "Zona 4")]
        Zona4 = 4,

        /// <summary>
        /// Representa la Zona 5, que es una zona libre.
        /// Es decir, no tiene restricciones específicas asociadas.
        /// </summary>
        [Display(Name = "Zona 5 - Libre")]
        Zona5 = 5
    }
}
