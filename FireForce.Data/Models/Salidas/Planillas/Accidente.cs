using FireForce.Shared.Enums;
using System.ComponentModel.DataAnnotations;

namespace FireForce.Data.Models.Salidas.Planillas
{
    public class Accidente : Salida
    {
        //Localización, datos del solicitante, personas damnificadas y datos del seguro

        public TipoAccidente Tipo { get; set; }
        
        public TipoCondicionesClimaticas CondicionesClimaticas { get; set; }

        [StringLength(255)]
        public string? OtroCondicion { get; set; }
    }
}
