using System.ComponentModel.DataAnnotations;

namespace Vista.Data.Enums
{
    public enum TipoMaquinaAgropecuaria
    {
        [Display(Name = "Tractor")]
        Tractor,

        [Display(Name = "Cosechadora")]
        Cosechadora,

        [Display(Name = "Sembradora")]
        Sembradora,

        [Display(Name = "Pulverizadora / Fumigadora")]
        Pulverizadora,

        [Display(Name = "Tolva autodescargable")]
        TolvaAutodescargable,

        [Display(Name = "Acoplado rural / carretones")]
        AcopladoRural,

        [Display(Name = "Mixer / carro forrajero")]
        MixerForrajero,

        [Display(Name = "Rotoenfardadora / enrolladora")]
        Rotoenfardadora,

        [Display(Name = "Segadora / acondicionadora")]
        Segadora,

        [Display(Name = "Rastrillo / hileradora")]
        Rastrillo,

        [Display(Name = "Arado / rastra / cincel")]
        AradoRastraCincel,

        [Display(Name = "Pala de arrastre / niveladora")]
        PalaArrastre,

        [Display(Name = "Tanque regador / cisterna")]
        TanqueCisterna,

        [Display(Name = "Extractor de granos / sinfín")]
        ExtractorGranos,

        [Display(Name = "Cargadora frontal / retroexcavadora")]
        CargadoraRetroexcavadora,

        [Display(Name = "Cuatriciclo / UTV rural")]
        CuatricicloUTV,

        [Display(Name = "Otro")]
        Otro
    }
}