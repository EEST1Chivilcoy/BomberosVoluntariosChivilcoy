using Vista.Data.Models.Salidas.Componentes;
using System.ComponentModel.DataAnnotations.Schema;
using Vista.Data.Models.Personas.Personal;

namespace Vista.Data.Models.Vehiculos
{
    public class Vehiculo_Personal : Vehiculo
    {
        public int PersonalId { get; set; }
        [ForeignKey("PersonalId")]
        public Personal Personal { get; set; } = null!;
    }
}
