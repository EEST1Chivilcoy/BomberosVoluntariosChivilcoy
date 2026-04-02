using System.ComponentModel.DataAnnotations;
using FireForce.Core.Data.Enums;
using FireForce.Core.Data.Models.Salidas.Componentes;
using FireForce.Core.Data.ViewModels.Personal;

namespace FireForce.Core.Data.ViewModels.Accidente
{
    public class AccidenteViewModels : SalidasViewModels
    {
        [Required]
        public TipoAccidente? Tipo { get; set; } = TipoAccidente.Transito;

        public TipoCondicionesClimaticas? CondicionesClimaticas { get; set; }
    }
}
