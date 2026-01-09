using Vista.Data.Enums.Socios;

namespace Vista.Data.Models.Socios.Componentes
{
    /// <summary>
    /// Representa un período de cuota de un socio.
    /// Registra la cuota vigente desde FechaDesde hasta FechaHasta.
    /// </summary>
    public class MovimientoCambioCuota : MovimientoSocio
    {
        /// <summary>
        /// Representa la frecuencia de pago durante este período.
        /// </summary>
        public FrecuenciaPago FrecuenciaDePago { get; set; }

        /// <summary>
        /// Representa la forma de pago durante este período.
        /// </summary>
        public FormaDePago FormaDePago { get; set; }

        /// <summary>
        /// Monto de la cuota durante este período.
        /// </summary>
        public double Monto { get; set; }
    }
}