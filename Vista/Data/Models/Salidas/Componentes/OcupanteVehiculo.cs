using System.ComponentModel.DataAnnotations.Schema;
using Vista.Data.Enums;
using Vista.Data.Models.Grupos.FuerzasIntervinientes;
using Vista.Data.Models.Personas;

namespace Vista.Data.Models.Salidas.Componentes
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
