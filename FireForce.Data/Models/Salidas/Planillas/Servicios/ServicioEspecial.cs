using FireForce.Shared.Enums;

namespace FireForce.Data.Models.Salidas.Planillas.Servicios
{
    public abstract class ServicioEspecial : Salida
    {
        public TipoOrganizacionBeneficiada TipoOrganizacion { get; set; }
    }
}
