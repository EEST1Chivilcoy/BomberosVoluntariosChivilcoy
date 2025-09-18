using System.ComponentModel.DataAnnotations;
using Vista.Data.Enums;
using Vista.Data.Models.Grupos.Brigadas;

namespace Vista.Data.ViewModels.Personal
{
    public class BomberoViweModel : PersonalViewModel
    {
        public EstadoBombero Estado { get; set; }

        [Required]
        public int NumeroLegajo { get; set; }

        public EscalafonJerarquico Grado { get; set; }

        public TipoDotaciones Dotacion { get; set; }
        
        public int? Altura { get; set; }
        public int? Peso { get; set; }
        public Brigada? Brigada { get; set; }
        public bool EsChofer { get; set; }
        public DateTime? VencimientoRegistro { get; set; }

        [StringLength(255)]
        public string? Observaciones { get; set; }
        [StringLength(255)]
        public string? Profesion { get; set; }
        [StringLength(255)]
        public string? NivelEstudios { get; set; }
        [StringLength(255)]
        public string? NumeroIoma { get; set; }
        
        //Brigada-------------------------------
        public int BrigadaId { get; set; }
        public string NombreBrigada { get; set; }
    }
}
