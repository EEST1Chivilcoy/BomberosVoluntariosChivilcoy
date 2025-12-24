using Vista.Data.Enums.Socios;

namespace Vista.Data.Models.Socios.Componentes
{
    /// <summary>
    /// Historial del costo de las cuotas de un socio.
    /// </summary>
    public class MovimientoCambioCuota : MovimientoSocio
    {
        /// <summary>
        /// Representa la frecuencia de pago anterior a la fecha del cambio.
        /// </summary>
        public FrecuenciaPago FrecuenciaDePagoAnterior { get; set; }

        /// <summary>
        /// Representa la forma de pago anterior a la fecha del cambio.
        /// </summary>
        public FormaDePago FormaDePagoAnterior { get; set; }

        /// <summary>
        /// Monto de la cuota anterior a la fecha del cambio.
        /// </summary>
        public double MontoAnterior { get; set; }
    }
}