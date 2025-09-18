using System.ComponentModel.DataAnnotations;

namespace Vista.Data.Enums.Socios
{
    /// <summary>
    /// Representa las diferentes frecuencias de pago disponibles para los socios.
    /// </summary>
    public enum FrecuenciaPago
    {
        /// <summary>
        /// Representa un pago mensual.
        /// </summary>
        [Display(Name = "Mensual")]
        Mensual = 1,

        /// <summary>
        /// Representa un pago trimestral.
        /// </summary>
        [Display(Name = "Trimestral")]
        Trimestral = 2,

        /// <summary>
        /// Representa un pago semestral.
        /// </summary>
        [Display(Name = "Semestral")]
        Semestral = 3,

        /// <summary>
        /// Representa un pago anual.
        /// </summary>
        [Display(Name = "Anual")]
        Anual = 4
    }
}
