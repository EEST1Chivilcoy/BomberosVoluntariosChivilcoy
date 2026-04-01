using Vista.Data.Enums;
using Vista.Data.Models.Salidas.Componentes;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Vista.Data.ViewModels.Personal;

namespace Vista.Data.Models.Salidas.Planillas
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
