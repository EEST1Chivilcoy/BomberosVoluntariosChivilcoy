using System.ComponentModel.DataAnnotations.Schema;
using Vista.Data.Models.Personas.Personal;

namespace Vista.Data.Models.Grupos.Brigadas
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
