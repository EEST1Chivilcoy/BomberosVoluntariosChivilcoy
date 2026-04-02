using System.ComponentModel.DataAnnotations.Schema;
using FireForce.Shared.Enums;
using FireForce.Data.Models.Personas.Personal;
using FireForce.Data.Models.Salidas.Planillas;
using FireForce.Data.Models.Vehiculos.Flota;

namespace FireForce.Data.Models.Salidas.Componentes
{
    public class BomberoSalida
    {
        public int BomberoSalidaId { get; set; }

        // Relación con Salida
        public int SalidaId { get; set; }

        [ForeignKey(nameof(SalidaId))]
        public Salida Salida { get; set; }

        // Relación con el Movil
        public int? MovilId { get; set; }

        public Movil? MovilAsignado { get; set; }

        public EscalafonJerarquico Grado { get; set; }

        // Relación con el Bombero
        public int PersonaId { get; set; }

        [ForeignKey("PersonaId")]
        public Bombero Bombero { get; set; }
    }
}
