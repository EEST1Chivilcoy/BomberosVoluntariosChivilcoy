using Vista.Data.Enums;
using Vista.Data.Models.Salidas.Planillas;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Permissions;
using Vista.Data.Models.Personas.Personal;
using Vista.Data.Models.Vehiculos.Flota;

namespace Vista.Data.Models.Salidas.Componentes
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
