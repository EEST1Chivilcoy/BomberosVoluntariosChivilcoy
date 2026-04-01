using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Vista.Data.Enums;
using Vista.Data.Models.Personas.Personal;

namespace Vista.Data.Models.Personas.Personal.Componentes
{
    public class AscensoBombero
    {
        public int AscensoBomberoId { get; set; }

        public DateOnly FechaAscenso { get; set; }


        [StringLength(255)]
        public string? Descripcion { get; set; }

        public int NumeroLibero { get; set; }

        public int NumeroActa { get; set; }

        public EscalafonJerarquico GradoAntiguo { get; set; }

        public EscalafonJerarquico GradoAscenso { get; set; }

        public int PersonaId { get; set; }
        [ForeignKey("PersonaId")]
        public Bombero PersonalAfectado { get; set; } = null!;
    }
}
