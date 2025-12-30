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

        /// <summary>
        /// Representa el motivo del cambio de estado del socio. (No es obligatorio)
        /// </summary>
        public string? Motivo { get; set; } = string.Empty;
    }
}