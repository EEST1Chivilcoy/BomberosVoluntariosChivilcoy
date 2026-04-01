using System.ComponentModel.DataAnnotations;

namespace Vista.Data.Enums
{
    public enum TipoIncendioTecho
    {
        [Display(Name = "Madera / Paja")]
        MaderaPaja,

        [Display(Name = "Yeso / Cielo raso")]
        YesoCieloRaso,

        [Display(Name = "Tejas cerámicas / coloniales")]
        Tejas,

        [Display(Name = "Chapa metálica")]
        ChapaMetalica,

        [Display(Name = "Chapa / Cartón")]
        ChapaCarton,

        [Display(Name = "Loza / Hormigón armado")]
        LozaHormigon,

        [Display(Name = "Fibrocemento")]
        Fibrocemento,

        [Display(Name = "PVC / Plástico")]
        PVCPlastico,

        [Display(Name = "Paneles térmicos")]
        PanelesTermicos,

        [Display(Name = "Otro")]
        Otro
    }
}