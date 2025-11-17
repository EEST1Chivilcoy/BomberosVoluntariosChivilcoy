using Vista.Data.Models.Vehiculos.Flota;

namespace Vista.Data.Models.Otros.Firmas
{

    public class IncidenteMovil : IncidenteBase
    {
        public VehiculoSalida Vehiculo { get; set; } = null!;
        public int VehiculoId { get; set; }
    }
}
