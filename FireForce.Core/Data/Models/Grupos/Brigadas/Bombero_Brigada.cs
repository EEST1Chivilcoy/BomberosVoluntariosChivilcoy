using System.ComponentModel.DataAnnotations.Schema;
using FireForce.Core.Data.Models.Personas.Personal;

namespace FireForce.Core.Data.Models.Grupos.Brigadas
{
    public class Bombero_Brigada
    {
        public int? PersonaId { get; set; }
        [ForeignKey(nameof(PersonaId))]
        public Bombero? Bombero { get; set; }

        public int? BrigadaId { get; set; }
        [ForeignKey(nameof(BrigadaId))]
        public Brigada? Brigada { get; set; }
    }
}
