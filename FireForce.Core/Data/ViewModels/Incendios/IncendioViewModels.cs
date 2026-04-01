using System.ComponentModel.DataAnnotations;
using Vista.Data.Enums;

namespace Vista.Data.ViewModels.Incendios;
using Vista.Data.Models.Salidas.Componentes;
public class IncendioViewModels : SalidasViewModels
{
    // --- Propiedades específicas para incendios ---

    /// <summary>
    /// Indica si el incendio fue detectado automáticamente por un sistema de detección.
    /// </summary>
    public bool DeteccionAutomatica { get; set; }

    /// <summary>
    /// Indica si habia un extintor disponible en el lugar del incendio.
    /// </summary>
    public bool Extintor { get; set; }

    /// <summary>
    /// Indica si habia un hidrante disponible en el lugar del incendio.
    /// </summary>
    public bool Hidrante { get; set; }

    /// <summary>
    /// Tipo de causa del incendio. (Accidental, Intencional, Natural, Desconocida)
    /// </summary>
    public TipoCausaIncendio SuperficieAfectadaCausa { get; set; }

    // --- Propiedades de evacuación ---

    /// <summary>
    /// Tipo de evacuación realizada durante el incendio. (No se evacuó, Evacuación Parcial, Evacuación Total)
    /// </summary>
    [Required]
    public TipoEvacuacion TipoEvacuacion { get; set; } = TipoEvacuacion.NoEvacuo;

    /// <summary>
    /// Tipo de superficie afectada por el incendio. (Kilómetro, Hectáreas, Metros)
    /// </summary>
    public TipoSuperficie? TipoSuperficieAfectada { get; set; }

    /// <summary>
    /// Tamaño de la superficie afectada por el incendio.
    /// </summary>
    public double? SuperficieAfectadaIncendio { get; set; }

    /// <summary>
    /// Detalle de la superficie afectada por el incendio.
    /// </summary>
    public string? DetalleSuperficieAfectadaIncendio { get; set; }

    // --- Materiales y estructuras ---

    /// <summary>
    /// Tipo de aberturas en la estructura afectada por el incendio.
    /// </summary>
    public TipoIncendioAbertura? TipoAbertura { get; set; }

    /// <summary>
    /// Otra especificación de aberturas en la estructura afectada por el incendio.
    /// </summary>
    [StringLength(255)]
    public string? OtraAbertura { get; set; }

    /// <summary>
    /// Tipo material del techo en la estructura afectada por el incendio.
    /// </summary>
    public TipoIncendioTecho? TipoTecho { get; set; }

    /// <summary>
    /// Otra especificación del material del techo en la estructura afectada por el incendio.
    /// </summary>
    [StringLength(255)]
    public string? OtroTecho { get; set; }

    /// <summary>
    /// Tipo material del piso en la estructura afectada por el incendio.
    /// </summary>
    public TipoMaterialPiso? TipoMaterialPiso { get; set; }

    /// <summary>
    /// Otra especificación del material del piso en la estructura afectada por el incendio.
    /// </summary>
    [StringLength(255)]
    public string? OtroMaterialPiso { get; set; }

    /// <summary>
    /// Nombre del establecimiento afectado por el incendio. Si no aplica, dejar en blanco.
    /// </summary>
    public string? NombreEstablecimiento { get; set; }

    /// <summary>
    /// Cantidad de pisos del edificio afectado por el incendio. Si no aplica, dejar en blanco.
    /// </summary>
    public int? CantidadPisos { get; set; }

    /// <summary>
    /// Cantidad del pisos afectados por el incendio. Si no aplica, dejar en blanco.
    /// </summary>
    public int? PisoAfectado { get; set; }

    /// <summary>
    /// Cantidad de ambientes afectados por el incendio. Se suman todos los ambientes afectados. Si no aplica, dejar en blanco.
    /// </summary>
    public int? CantidadAmbientes { get; set; }
}

