using FireForce.Core.Data.Models.Salidas.Componentes;
using System.ComponentModel.DataAnnotations.Schema;
using FireForce.Core.Data.Models.Personas.Personal;

namespace FireForce.Core.Data.Models.Vehiculos
{
    public class Vehiculo_Personal : Vehiculo
    {
        public int PersonalId { get; set; }
        [ForeignKey("PersonalId")]
        public Personal Personal { get; set; } = null!;
    }
}
