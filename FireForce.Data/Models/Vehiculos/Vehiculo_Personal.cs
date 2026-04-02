using System.ComponentModel.DataAnnotations.Schema;
using FireForce.Data.Models.Personas.Personal;

namespace FireForce.Data.Models.Vehiculos
{
    public class Vehiculo_Personal : Vehiculo
    {
        public int PersonalId { get; set; }
        [ForeignKey("PersonalId")]
        public Personal Personal { get; set; } = null!;
    }
}
