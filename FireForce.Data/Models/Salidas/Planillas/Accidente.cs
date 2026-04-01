using FireForce.Core.Data.Enums;
using FireForce.Core.Data.Models.Salidas.Componentes;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using FireForce.Core.Data.ViewModels.Personal;

namespace FireForce.Core.Data.Models.Salidas.Planillas
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
