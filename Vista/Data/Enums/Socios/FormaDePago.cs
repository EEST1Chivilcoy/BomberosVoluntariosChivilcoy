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
        Transferencia = 2,

        // --- Otros métodos de pago --- (Borrar luego de la presentación)

        [Display(Name = "Tarjeta de Débito / Crédito")]
        Tarjeta = 3,

        [Display(Name = "Cheque")]
        Cheque = 4,

        [Display(Name = "Billetera Virtual")]
        BilleteraVirtual = 5,

        [Display(Name = "Depósito Bancario")]
        DepositoBancario = 6,
    }
}
