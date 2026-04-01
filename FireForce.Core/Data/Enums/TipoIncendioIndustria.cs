using System.ComponentModel.DataAnnotations;

namespace Vista.Data.Enums
{
    public enum TipoIncendioIndustria
    {
        [Display(Name = "Metalmecánica / Metalúrgica")]
        MetalmecanicaMetalurgica,

        [Display(Name = "Textil / Confección")]
        TextilConfeccion,

        [Display(Name = "Plásticos / Poliuretano")]
        PlasticosPoliuretano,

        [Display(Name = "Agroindustria / Alimentos")]
        AgroindustriaAlimentos,

        [Display(Name = "Molinería / Harinas")]
        MolineriaHarinas,

        [Display(Name = "Logística / Depósitos")]
        LogisticaDepositos,

        [Display(Name = "Química / Pinturas")]
        QuimicaPinturas,

        [Display(Name = "Automotriz / Autopartes")]
        AutomotrizAutopartes,

        [Display(Name = "Electromecánica / Electricidad")]
        ElectromecanicaElectricidad,

        [Display(Name = "Carpintería / Madera")]
        CarpinteriaMadera,

        [Display(Name = "Imprenta / Gráfica")]
        ImprentaGrafica,

        [Display(Name = "Otro")]
        Otro
    }
}