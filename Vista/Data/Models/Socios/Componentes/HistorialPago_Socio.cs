using Vista.Data.Enums;
using Vista.Data.Models.Socios;
using Vista.Data.Models.Socios.Componentes;
using Vista.Data.Enums.Socios;
using Vista.Data.Models.Personas.Personal;

namespace Vista.Data.Models.Socios.Componentes
{
    /// <summary>
    /// Representa el historial de pagos realizados por un socio.
    /// </summary>
    public class HistorialPago_Socio : Historial_Socio
    {
        /// <summary>
        /// Representa la forma de pago utilizada por el socio en el pago. (Efectivo, Tarjeta, Transferencia, etc.) (Por defecto es Efectivo)
        /// </summary>
        public FormaDePago FormaDePago { get; set; } = FormaDePago.Efectivo;

        /// <summary>
        /// Cobrador que realizó el cobro al socio.
        /// </summary>
        public Cobrador CobradorQueCobro { get; set; } = null!;

        /// <summary>
        /// Representa el monto pagado por el socio.
        /// </summary>
        public double Monto { get; set; }
    }
}
