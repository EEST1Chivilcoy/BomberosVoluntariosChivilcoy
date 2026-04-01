using System.ComponentModel.DataAnnotations;

namespace Vista.Data.Enums
{
    public enum TipoIncendioForestal
    {
        [Display(Name = "Campo agrícola")]
        CampoAgricola,

        [Display(Name = "Pastizal natural")]
        PastizalNatural,

        [Display(Name = "Zona periurbana / Interfase")]
        ZonaPeriurbanaInterfase,

        [Display(Name = "Cortina forestal / Monte cultivado")]
        CortinaForestalMonteCultivado,

        [Display(Name = "Banquina / Camino rural")]
        BanquinaCaminoRural,

        [Display(Name = "Arbustal / Matorral")]
        ArbustalOMatorral,

        [Display(Name = "Basural / Quema de residuos")]
        BasuralQuemaResiduos,

        [Display(Name = "Zona de laguna / humedal")]
        ZonaLagunaHumedal,

        [Display(Name = "Árbol urbano / vegetación de calle")]
        ArbolUrbanoVegetacionCalle,

        [Display(Name = "Otro")]
        Otro
    }
}