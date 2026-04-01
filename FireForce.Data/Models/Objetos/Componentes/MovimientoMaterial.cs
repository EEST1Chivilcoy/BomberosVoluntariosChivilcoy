using FireForce.Core.Data.Enums;
using FireForce.Core.Data.Models.Objetos;
using FireForce.Core.Data.Models.Personas.Personal;
using FireForce.Core.Data.Models.Vehiculos.Flota;

namespace FireForce.Core.Data.Models.Objetos.Componentes
{
    public class MovimientoMaterial
    {
        public int MovimientoMaterialId { get; set; }
        public DateTime FechaIngreso { get; set; }
        public string? Observaciones { get; set; }
        public TipoMovimiento TipoMovimiento { get; set; }
        public Bombero? DestinoBombero { get; set; }
        public Movil? DestinoMovil { get; set; }
        public int Cantidad { get; set; }
        public Material? Materiales { get; set; }
        public bool esComisionDirectiva { get; set; } = false;
    }
}
