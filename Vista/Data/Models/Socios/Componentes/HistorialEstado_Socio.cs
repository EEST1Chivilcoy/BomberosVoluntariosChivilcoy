using Vista.Data.Enums.Socios;

namespace Vista.Data.Models.Socios.Componentes
{
    /// <summary>
    /// Representa el historial de un socio, incluyendo fechas de alta y baja, motivo de baja.
    /// </summary>
    public class HistorialEstado_Socio : Historial_Socio
    {
        /// <summary>
        /// Representa la fecha del evento en el historial del socio.
        /// </summary>
        public DateTime Fecha { get; set; }

        /// <summary>
        /// Representa el estado del socio en el historial.
        /// </summary>
        public TipoEstadoSocio Estado { get; set; }

        /// <summary>
        /// Representa el motivo del cambio de estado del socio. (No es obligatorio)
        /// </summary>
        public string? Motivo { get; set; } = string.Empty;
    }
}