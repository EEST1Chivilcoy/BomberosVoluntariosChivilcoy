using FireForce.Data.Models.Personas;
using FireForce.Shared.Enums.Firmas;

namespace FireForce.Data.Models.Otros.Firmas
{
    /// <summary>
    /// Entrada de la bitácora de auditoría de un incidente: quién hizo qué y cuándo.
    /// </summary>
    public class MovimientoIncidente
    {
        public int Id { get; set; }

        public string? Descripcion { get; set; }

        public DateTime Fecha { get; set; } = DateTime.Now;

        public TipoMovimiento Tipo { get; set; }

        public int IncidenteId { get; set; }

        public IncidenteBase Incidente { get; set; } = null!;

        public int AutorId { get; set; }

        /// <summary>
        /// Quien originó el movimiento. Es <see cref="Persona"/> y no Bombero porque el movimiento
        /// de <see cref="TipoMovimiento.Creacion"/> lo hace el solicitante, que puede no ser bombero.
        /// </summary>
        public Persona Autor { get; set; } = null!;
    }
}
