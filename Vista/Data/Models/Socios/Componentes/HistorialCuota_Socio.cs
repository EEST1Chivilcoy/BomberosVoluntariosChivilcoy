using Vista.Data.Enums.Socios;

namespace Vista.Data.Models.Socios.Componentes
{
    /// <summary>
    /// Historial del costo de las cuotas de un socio.
    /// </summary>
    public class HistorialCuota_Socio : Historial_Socio
    {
        /// <summary>
        /// Representa la frecuencia de pago anterior a la fecha del cambio.
        /// </summary>
        public FrecuenciaPago FrecuenciaDePagoAnterior { get; set; }

        /// <summary>
        /// Monto de la cuota anterior a la fecha del cambio.
        /// </summary>
        public double MontoAnterior { get; set; }

        /// <summary>
        /// Representa la nueva frecuencia de pago a partir de la fecha del cambio.
        /// </summary>
        public FrecuenciaPago FrecuenciaDePagoNueva { get; set; }

        /// <summary>
        /// Monto de la nueva cuota a partir de la fecha del cambio.
        /// </summary>
        public double MontoNuevo { get; set; }
    }
}
