using System.ComponentModel.DataAnnotations;

namespace Vista.Data.Enums
{
    public enum TipoMaterialPiso
    {
        [Display(Name = "Cemento alisado")]
        CementoAlisado,

        [Display(Name = "Hormigón")]
        Hormigon,

        [Display(Name = "Cerámico / Porcelanato")]
        CeramicoPorcelanato,

        [Display(Name = "Mosaico")]
        Mosaico,

        [Display(Name = "Baldosa")]
        Baldosa,

        [Display(Name = "Ladrillo")]
        Ladrillo,

        [Display(Name = "Madera / Parquet")]
        MaderaParquet,

        [Display(Name = "Flotante / Melamínico")]
        FlotanteMelaminico,

        [Display(Name = "Vinílico / PVC")]
        VinilicoPVC,

        [Display(Name = "Alfombra")]
        Alfombra,

        [Display(Name = "Tierra / Piso natural")]
        TierraNatural,

        [Display(Name = "Plástico / Goma")]
        PlasticoGoma,

        [Display(Name = "Otro")]
        Otro
    }
}