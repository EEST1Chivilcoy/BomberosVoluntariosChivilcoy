using Vista.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace Vista.Data.Models.Salidas.Planillas
{
    public class FactorClimatico : Salida
    {
        //Localización, datos del solicitante, personas damnificadas y datos del seguro

        public TipoFactoresClimaticos Tipo { get; set; } // TODO: Consultar a Agustín

        //Daños superficie evacuada
        public TipoEvacuacion Evacuacion { get; set; } // TODO: Consultar a Agustín
        public TipoSuperficie Superficie { get; set; }
        public int CantidadAfectadaFactorClimatico { get; set; }
    }
}
