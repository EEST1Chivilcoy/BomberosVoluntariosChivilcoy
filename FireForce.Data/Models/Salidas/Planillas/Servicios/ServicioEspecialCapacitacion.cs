using System.ComponentModel.DataAnnotations;
using FireForce.Core.Data.Enums;

namespace FireForce.Core.Data.Models.Salidas.Planillas.Servicios
{
    public class ServicioEspecialCapacitacion : ServicioEspecial
    {
        public TipoNivelCapacitacion NivelDeCapacitacion { get; set; }
        public TipoCapacitacion TipoCapacitacion { get; set; }
        public DateTime DiaHora { get; set; }
        [Required, StringLength(255)]
        public string TipoCapacitacionOtra { get; set; }
        [Required, StringLength(255)]
        public string NivelDeCapacitacionOtro { get; set; }

    }
}
