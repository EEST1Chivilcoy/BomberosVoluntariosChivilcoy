using System.ComponentModel.DataAnnotations;

namespace Vista.Data.Enums
{
    public enum TipoIncendioEstablecimientoEducativo
    {
        [Display(Name = "Jardín")]
        Jardin,

        [Display(Name = "Escuela Primaria")]
        EscuelaPrimaria,

        [Display(Name = "Escuela Secundaria")]
        EscuelaSecundaria,

        [Display(Name = "Escuela Técnica")]
        EscuelaTecnica,

        [Display(Name = "Escuela Rural")]
        EscuelaRural,

        [Display(Name = "Escuela Especial")]
        EscuelaEspecial,

        [Display(Name = "Centro de Formación Profesional")]
        CentroFormacionProfesional,

        [Display(Name = "Instituto Terciario")]
        InstitutoTerciario,

        [Display(Name = "Universidad")]
        Universidad,

        [Display(Name = "Centro de Capacitación")]
        CentroCapacitacion,

        [Display(Name = "Academia / Instituto Privado")]
        AcademiaInstitutoPrivado,

        [Display(Name = "Escuela de Arte / Música / Danza")]
        EscuelaArteMusicaDanza,

        [Display(Name = "Academia de Idiomas")]
        AcademiaIdiomas,

        [Display(Name = "Otro")]
        Otro
    }
}