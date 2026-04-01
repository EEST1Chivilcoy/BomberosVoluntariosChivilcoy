using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Vista.Data.Models.Salidas.Planillas.Incendios;

namespace Vista.Data.Models.Vehiculos.Flota
{
    public class EmbarcacionAfectada
    {
        public int EmbarcacionAfectadaId { get; set; }

        public bool Intervinientes { get; set; }
        public int CantidadBarcos { get; set; }
        public int CantidadBotes { get; set; }
        public int CantidadFragatas { get; set; }
        public int CantidadLanchas { get; set; }
        [StringLength(255)]
        public string? Otro { get; set; }

        public int SalidaId { get; set; }
        [ForeignKey("SalidaId")]
        public Incendio Incendio { get; set; }
    }
}
