using System.ComponentModel.DataAnnotations;

namespace Vista.Data.Enums
{
    public enum TipoIncendioAbertura
    {
        [Display(Name = "Madera")]
        Madera,

        [Display(Name = "Acero / Hierro")]
        AceroHierro,

        [Display(Name = "Aluminio")]
        Aluminio,

        [Display(Name = "Plástico")]
        Plastica,

        [Display(Name = "PVC / Termofusión")]
        PVC,

        [Display(Name = "Vidrio / Blindex")]
        VidrioBlindex,

        [Display(Name = "Chapa / Panel metálico")]
        ChapaPanelMetalico,

        [Display(Name = "Combinada (mixta)")]
        Combinada,

        [Display(Name = "Otro")]
        Otro
    }
}