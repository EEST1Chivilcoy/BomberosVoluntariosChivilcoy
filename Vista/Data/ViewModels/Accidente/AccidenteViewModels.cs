using System.ComponentModel.DataAnnotations;
using Vista.Data.Enums;
using Vista.Data.Models.Salidas.Componentes;
using Vista.Data.ViewModels.Personal;

namespace Vista.Data.ViewModels.Accidente
{
    public class AccidenteViewModels : SalidasViewModels
    {
        [Required]
        public TipoAccidente? Tipo { get; set; } = TipoAccidente.Transito;

        public TipoCondicionesClimaticas? CondicionesClimaticas { get; set; }
    }
}
