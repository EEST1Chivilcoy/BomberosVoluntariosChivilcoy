using System.ComponentModel.DataAnnotations;

namespace Vista.Data.Enums
{
    public enum TipoCausaIncendio
    {
        [Display(Name = "Negligencia (descuidos, colillas, fogones)")]
        Negligencia,

        [Display(Name = "Intencional / Deliberado")]
        Intencional,

        [Display(Name = "Natural (rayos, calor extremo)")]
        Natural,

        [Display(Name = "Corto circuito / Falla eléctrica")]
        FallaElectrica,

        [Display(Name = "Sobrecalentamiento de artefactos")]
        Sobrecalentamiento,

        [Display(Name = "Fuga de gas / explosión")]
        FugaDeGas,

        [Display(Name = "Manipulación de combustibles")]
        Combustibles,

        [Display(Name = "Quema de residuos / basura")]
        QuemaDeResiduos,

        [Display(Name = "Uso de pirotecnia")]
        Pirotecnia,

        [Display(Name = "Accidente vehicular")]
        AccidenteVehicular,

        [Display(Name = "Desconocida")]
        Desconocida
    }
}