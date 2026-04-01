using FireForce.Core.Data.Enums;
using FireForce.Core.Data.Models.Salidas.Componentes;
using System.ComponentModel.DataAnnotations;

namespace FireForce.Core.Data.Models.Salidas.Planillas.Servicios
{
    public abstract class ServicioEspecial : Salida
    {
        public TipoOrganizacionBeneficiada TipoOrganizacion { get; set; }
    }
}
