using FireForce.Core.Data.Models.Vehiculos.Flota;

namespace FireForce.Core.Data.Models.Otros.Firmas
{

    public class IncidenteMovil : IncidenteBase
    {
        public VehiculoSalida Vehiculo { get; set; } = null!;
        public int VehiculoId { get; set; }
    }
}
