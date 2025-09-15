using System.ComponentModel.DataAnnotations;

namespace Vista.Data.Enums.Socios
{
    /// <summary>
    /// Representa las zonas geográficas o áreas de operación de los socios.
    /// Esto es paramarcar diferentes regiones o sectores en los que los socios pueden estan ubicados.
    /// </summary>
    [Flags]
    public enum Zona
    {
        /// <summary>
        /// Sin zonas asignadas.
        /// </summary>
        [Display(Name = "Ninguna")]
        Ninguna = 0,

        /// <summary>
        /// Representa la Zona 1.
        /// </summary>
        [Display(Name = "Zona 1")]
        Zona1 = 1 << 0, // 1

        /// <summary>
        /// Representa la Zona 2.
        /// </summary>
        [Display(Name = "Zona 2")]
        Zona2 = 1 << 1, // 2

        /// <summary>
        /// Representa la Zona 3.
        /// </summary>
        [Display(Name = "Zona 3")]
        Zona3 = 1 << 2, // 4

        /// <summary>
        /// Representa la Zona 4.
        /// </summary>
        [Display(Name = "Zona 4")]
        Zona4 = 1 << 3, // 8

        /// <summary>
        /// Representa la Zona 5, que es una zona libre.
        /// </summary>
        [Display(Name = "Zona 5 - Libre")]
        Zona5 = 1 << 4  // 16
    }
}
