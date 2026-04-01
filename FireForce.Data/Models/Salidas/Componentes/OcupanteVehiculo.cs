using System.ComponentModel.DataAnnotations.Schema;
using FireForce.Core.Data.Enums;
using FireForce.Core.Data.Models.Grupos.FuerzasIntervinientes;
using FireForce.Core.Data.Models.Personas;

namespace FireForce.Core.Data.Models.Salidas.Componentes
{
    public class OcupanteVehiculo
    {
        public int OcupanteVehiculoId { get; set; }

        public int VehiculoAfectadoId { get; set; }

        [ForeignKey(nameof(VehiculoAfectadoId))]
        public VehiculoAfectado VehiculoAfectado { get; set; } = null!;

        public int DamnificadoSalidaId { get; set; }

        [ForeignKey(nameof(DamnificadoSalidaId))]
        public Damnificado_Salida Damnificado { get; set; } = null!;

        public TipoOcupante Rol { get; set; } // Enum: Conductor, Pasajero
    }
}
