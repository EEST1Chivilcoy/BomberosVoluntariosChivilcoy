using System.ComponentModel.DataAnnotations;
using FireForce.Shared.Enums;

namespace FireForce.Client.Data.ViewModels.Accidente
{
    public class AccidenteViewModels : SalidasViewModels
    {
        [Required]
        public TipoAccidente? Tipo { get; set; } = TipoAccidente.Transito;

        public TipoCondicionesClimaticas? CondicionesClimaticas { get; set; }
    }
}
