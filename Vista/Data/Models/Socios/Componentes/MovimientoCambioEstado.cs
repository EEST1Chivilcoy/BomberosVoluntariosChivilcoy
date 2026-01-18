using Vista.Data.Enums.Socios;

namespace Vista.Data.Models.Socios.Componentes
{
    /// <summary>
    /// Representa un período de estado de un socio.
    /// Registra el estado vigente desde FechaDesde hasta FechaHasta.
    /// </summary>
    public class MovimientoCambioEstado : MovimientoSocio
    {
        /// <summary>
        /// Representa el estado del socio durante este período.
        /// </summary>
        public TipoEstadoSocio Estado { get; set; }
    }
}