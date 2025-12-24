using Vista.Data.Enums.Socios;

namespace Vista.Data.Models.Socios.Componentes
{
    /// <summary>
    /// Representa el historial de un socio, incluyendo fechas de alta y baja, motivo de baja.
    /// </summary>
    public class MovimientoCambioEstado : MovimientoSocio
    {
        /// <summary>
        /// Representa la fecha del evento en el historial del socio.
        /// </summary>
        public DateTime Fecha { get; set; }

        /// <summary>
        /// Representa el Estado anterior del socio antes del cambio.
        /// </summary>
        public TipoEstadoSocio EstadoAnterior { get; set; }

        /// <summary>
        /// Representa el motivo del cambio de estado del socio. (No es obligatorio)
        /// </summary>
        public string? Motivo { get; set; } = string.Empty;
    }
}