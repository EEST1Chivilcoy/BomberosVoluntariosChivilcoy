using System.ComponentModel.DataAnnotations;

namespace Vista.Data.Enums
{
    public enum TipoServicioPrevencion
    {
        [Display(Name = "Aterrizaje")]
        Aterrizaje,

        [Display(Name = "Despegue")]
        Despegue,

        [Display(Name = "Recital / espectáculo musical")]
        Recital,

        [Display(Name = "Evento institucional / protocolar")]
        EventoInstitucional,

        [Display(Name = "Fiesta popular / barrial")]
        FiestaPopular,

        [Display(Name = "Acto escolar / educativo")]
        ActoEscolar,

        [Display(Name = "Competencia deportiva / carrera")]
        CompetenciaDeportiva,

        [Display(Name = "Procesión / evento religioso")]
        EventoReligioso,

        [Display(Name = "Feria / exposición")]
        FeriaExposicion,

        [Display(Name = "Desfile / caravana")]
        DesfileCaravana,

        [Display(Name = "Simulacro / ejercicio conjunto")]
        SimulacroEjercicio,

        [Display(Name = "Otro")]
        Otro
    }
}