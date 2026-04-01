using Vista.Data.Enums;
using Vista.Data.Models.Salidas.Componentes;
using System.ComponentModel.DataAnnotations;

namespace Vista.Data.Models.Salidas.Planillas.Servicios
{
    public abstract class ServicioEspecial : Salida
    {
        public TipoOrganizacionBeneficiada TipoOrganizacion { get; set; }
    }
}
