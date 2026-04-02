using System.ComponentModel.DataAnnotations;
using FireForce.Shared.Enums;

namespace FireForce.Client.Data.ViewModels.FactorClimatico
{
    public class FactorClimaticoViewModel : SalidasViewModels
    {
        [Required]
        public TipoFactoresClimaticos Tipo { get; set; }

        public TipoEvacuacion? Evacuacion { get; set; }
        public TipoSuperficie? Superficie { get; set; }
        public int? CantidadAfectada { get; set; }
    }
}
