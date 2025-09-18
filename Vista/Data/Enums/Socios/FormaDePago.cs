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
        /// Representa el pago mediante tarjeta de crédito.
        /// </summary>
        [Display(Name = "Tarjeta de Crédito")]
        TarjetaDeCredito = 2,

        /// <summary>
        /// Representa el pago mediante tarjeta de débito.
        /// </summary>
        [Display(Name = "Tarjeta de Débito")]
        TarjetaDeDebito = 3,

        /// <summary>
        /// Representa el pago mediante transferencia bancaria.
        /// </summary>
        [Display(Name = "Transferencia Bancaria")]
        TransferenciaBancaria = 4,

        /// <summary>
        /// Representa el pago mediante cheque.
        /// </summary>
        [Display(Name = "Cheque")]
        Cheque = 5
    }
}
