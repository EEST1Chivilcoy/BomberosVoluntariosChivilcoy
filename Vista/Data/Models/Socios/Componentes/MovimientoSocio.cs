namespace Vista.Data.Models.Socios.Componentes
{
    public abstract class MovimientoSocio
    {
        /// <summary>
        /// Identificador único del historial.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Fecha desde la cual el movimiento está vigente.
        /// </summary>
        public DateTime FechaDesde { get; set; } = DateTime.Now;

        /// <summary>
        /// Fecha hasta la cual el movimiento estuvo vigente.
        /// Si es null, significa que el movimiento sigue vigente.
        /// </summary>
        public DateTime? FechaHasta { get; set; }

        /// <summary>
        /// Representa el identificador del socio asociado a este historial.
        /// </summary>
        public int SocioId { get; set; }

        /// <summary>
        /// Representa la relación del historial con el socio. (Muchos a 1) (Navegación inversa)
        /// </summary>
        public Socio Socio { get; set; } = null!;

        /// <summary>
        /// Razón o motivo del cambio registrado en este movimiento.
        /// </summary>
        public string? Motivo { get; set; }
    }
}
