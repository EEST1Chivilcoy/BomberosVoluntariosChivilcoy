using System.ComponentModel.DataAnnotations.Schema;
using FireForce.Data.Models.Personas.Personal;

namespace FireForce.Data.Models.Grupos.Guardia
{
    public class Bombero_Guardia
    {
        public int PersonaId { get; set; }
        [ForeignKey(nameof(PersonaId))]
        public Bombero Bombero { get; set; }

        public int GuardiaId { get; set; }
        [ForeignKey(nameof(GuardiaId))]
        public Guardia Guardia { get; set; }
    }
}
