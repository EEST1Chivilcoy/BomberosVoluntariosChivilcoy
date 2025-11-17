using System.ComponentModel.DataAnnotations;

namespace Vista.Data.Enums
{
    public enum TipoIncendioAeronaves
    {
        [Display(Name = "Avión")]
        Avion,

        [Display(Name = "Avión de carga")]
        AvionCarga,

        [Display(Name = "Avión comercial")]
        AvionComercial,

        [Display(Name = "Avión militar")]
        AvionMilitar,

        [Display(Name = "Helicóptero")]
        Helicoptero,

        [Display(Name = "Helicóptero militar")]
        HelicopteroMilitar,

        [Display(Name = "Dron")]
        Dron,

        [Display(Name = "Dron militar")]
        DronMilitar,

        [Display(Name = "Avión privado")]
        AvionPrivado,

        [Display(Name = "Planeador")]
        Planeador,

        [Display(Name = "Ultraliviano")]
        Ultraliviano,

        [Display(Name = "Globo aerostático")]
        GloboAerostatico,

        [Display(Name = "Jet ejecutivo")]
        JetEjecutivo,

        [Display(Name = "Avión experimental")]
        AvionExperimental,

        [Display(Name = "Otro")]
        Otro
    }
}