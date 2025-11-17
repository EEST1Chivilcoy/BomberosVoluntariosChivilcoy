using System.ComponentModel.DataAnnotations;

namespace Vista.Data.Enums.Socios
{
    /// <summary>
    /// Representa las diferentes formas de pago disponibles para las transacciones.
    /// Esta enumeración se utiliza para categorizar y gestionar los métodos de pago aceptados.
    /// </summary>
    public enum FormaDePago
    {
        /// <summary>
        /// Representa el pago en efectivo.
        /// </summary>
        [Display(Name = "Efectivo")]
        Efectivo = 1,

        /// <summary>
        /// Representa el pago mediante transferencia bancaria.
        /// </summary>
        [Display(Name = "Transferencia")]
        Transferencia = 2
    }
}
