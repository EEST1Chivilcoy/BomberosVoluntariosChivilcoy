using Vista.Data.Enums;
using Vista.Data.Models.Salidas.Componentes;
using System.ComponentModel.DataAnnotations;

namespace Vista.Data.Models.Salidas.Planillas.Incendios
{
    public class Incendio : Salida
    {
        //Localización, datos del solicitante, personas damnificadas y datos del seguro
        public bool DeteccionAutomaticaId { get; set; }
        public bool Extintor { get; set; }
        public bool Hidrante { get; set; }
        public TipoEvacuacion TipoEvacuacion { get; set; }
        [Required]
        public TipoSuperficie TipoSuperficieAfectada { get; set; }
        [StringLength(255)]
        public string? DetalleSuperficieAfectadaIncendio { get; set; }
        public TipoCausaIncendio SuperficieAfectadaCausa { get; set; }

        //Fin Superficie Afectada

        public TipoIncendioAbertura? TipoAbertura { get; set; }
        [StringLength(255)]
        public string? OtraAbertura { get; set; }
        public TipoIncendioTecho? TipoTecho { get; set; }
        [StringLength(255)]
        public string? OtroTecho { get; set; }
        [StringLength(255)]
        public string? OtroLugarIncendio { get; set; }
        [StringLength(255)]
        public string? NombreEstablecimiento { get; set; }
        public int? CantidadPisos { get; set; }
        public int? PisoAfectado { get; set; }
        public int? CantidadAmbientes { get; set; }
    }
}
