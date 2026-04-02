using FireForce.Shared.Enums;

namespace FireForce.Data.Models.Salidas.Planillas.Incendios
{
    public class IncendioComercio : Incendio
    {
        public TipoLugarComercioIncendio TipoLugar { get; set; }
    }
}
