using System.ComponentModel.DataAnnotations;

namespace Vista.Data.Enums
{
    public enum TipoIncendioVivienda
    {
        [Display(Name = "Casa")]
        Casa,

        [Display(Name = "Departamento")]
        Depto,

        [Display(Name = "Edificio")]
        Edificio,

        [Display(Name = "Casilla")]
        Casilla,

        [Display(Name = "Rancho")]
        Rancho,

        [Display(Name = "Vivienda multifamiliar")]
        Multifamiliar,

        [Display(Name = "Quinta")]
        Quinta,

        [Display(Name = "Vivienda rural")]
        ViviendaRural,

        [Display(Name = "Propiedad horizontal (PH)")]
        PH,

        [Display(Name = "Vivienda abandonada / ocupada")]
        ViviendaAbandonada,

        [Display(Name = "Residencia de adultos mayores")]
        ResidenciaAdultosMayores,

        [Display(Name = "Otro")]
        Otro
    }
}