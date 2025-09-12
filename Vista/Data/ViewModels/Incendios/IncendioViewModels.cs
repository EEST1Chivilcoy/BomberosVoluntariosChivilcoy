using System.ComponentModel.DataAnnotations;
using Vista.Data.Enums;

namespace Vista.Data.ViewModels.Incendios;
using Vista.Data.Models.Salidas.Componentes;
public class IncendioViewModels : SalidasViewModels
{
    public bool DeteccionAutomaticaId { get; set; }
    public bool Extintor { get; set; }

    public bool Hidrante { get; set; }


    public TipoEvacuacion TipoEvacuacion { get; set; }
    public TipoSuperficie TipoSuperficieAfectada { get; set; }

    public string? DetalleSuperficieAfectadaIncendio { get; set; }
    public TipoCausaIncendio SuperficieAfectadaCausa { get; set; }
    public TipoIncendioAbertura? TipoAbertura { get; set; }
    [StringLength(255)]
    public string? OtraAbertura { get; set; }
    public TipoIncendioTecho? TipoTecho { get; set; }
    [StringLength(255)]
    public string? OtroTecho { get; set; }
    [StringLength(255)]
    public string? OtroLugar { get; set; }
    public string? NombreEstablecimiento { get; set; }
    public int? CantidadPisos { get; set; }
    public int? PisoAfectado { get; set; }
    public int? CantidadAmbientes { get; set; }
}

