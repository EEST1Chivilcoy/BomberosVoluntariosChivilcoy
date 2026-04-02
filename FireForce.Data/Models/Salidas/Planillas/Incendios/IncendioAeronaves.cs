using FireForce.Shared.Enums;

namespace FireForce.Data.Models.Salidas.Planillas.Incendios
{
    public class IncendioAeronaves : Incendio
    {
        public TipoIncendioAeronaves TipoAeronave { get; set; }

        // TODO: Hacer igual que vehiculos (múltiples)
    }
}
