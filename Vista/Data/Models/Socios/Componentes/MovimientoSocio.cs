namespace Vista.Data.Models.Socios.Componentes
{
    public abstract class MovimientoSocio
    {
        /// <summary>
        /// Identificador único del historial.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Fecha en que se realizó el cambio registrado en el historial.
        /// </summary>
        public DateTime FechaDeCambio { get; set; } = DateTime.Now;

        /// <summary>
        /// Representa el identificador del socio asociado a este historial.
        /// </summary>
        public int SocioId { get; set; }

        /// <summary>
        /// Representa la relación del historial con el socio. (Muchos a 1) (Navegación inversa)
        /// </summary>
        public Socio Socio { get; set; } = null!;
    }
}
