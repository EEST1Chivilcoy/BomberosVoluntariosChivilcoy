using Vista.Data.Enums;
using Vista.Data.Models.Objetos;
using Vista.Data.Models.Personas.Personal;
using Vista.Data.Models.Vehiculos.Flota;

namespace Vista.Data.Models.Objetos.Componentes
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
