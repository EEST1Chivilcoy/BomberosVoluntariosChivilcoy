using System.ComponentModel.DataAnnotations;

namespace Vista.Data.Enums
{
    public enum TipoServicioRepresentaciones
    {
        [Display(Name = "Desfiles")]
        Desfiles,

        [Display(Name = "Honores funerarios")]
        HonoresFunerarios,

        [Display(Name = "Aniversarios institucionales")]
        Aniversarios,

        [Display(Name = "Eventos públicos")]
        EventosPublicos,

        [Display(Name = "Eventos privados")]
        EventosPrivados,

        [Display(Name = "Ceremonias protocolares")]
        Ceremonias,

        [Display(Name = "Actos escolares / educativos")]
        ActosEscolares,

        [Display(Name = "Procesiones / eventos religiosos")]
        ProcesionesReligiosas,

        [Display(Name = "Reconocimientos / entregas de distinciones")]
        ReconocimientosDistinciones,

        [Display(Name = "Capacitaciones / charlas comunitarias")]
        CapacitacionesCharlas,

        [Display(Name = "Simulacros / ejercicios conjuntos")]
        SimulacrosEjercicios,

        [Display(Name = "Otro")]
        Otro
    }
}