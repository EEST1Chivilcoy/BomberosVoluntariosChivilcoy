using System.ComponentModel.DataAnnotations;
namespace Vista.Data.Enums
{
    public enum TipoCapacitacion
    {
        [Display(Name = "Rescate con Cuerda")]
        RescateConCuerda,

        [Display(Name = "Rescate en Altura")]
        RescateEnAltura,

        [Display(Name = "Comando de Incidente")]
        ComandoDeIncidente,

        [Display(Name = "Estructuras Colapsadas")]
        EstructurasColapsadas,

        [Display(Name = "Incendios Estructurales")]
        IncendiosEstructurales,

        [Display(Name = "Incendios Forestales")]
        IncendiosForestales,

        [Display(Name = "Materiales Peligrosos")]
        MaterialesPeligrosos,

        [Display(Name = "Pedagogía")]
        PedagogiaYDidactiva,

        [Display(Name = "Psicologia de la Emergencia")]
        PscologiaDeLaEmergencia,

        [Display(Name = "Rescate Acuático")]
        RescateAcuatico,

        [Display(Name = "Rescate Vehicular")]
        RescateVehicular,

        [Display(Name = "Socorrismo")]
        Socorrismo,

        [Display(Name = "Departamento de Operaciones")]
        DepartamentoDeOperaciones,

        [Display(Name = "Escuela de Cadetes")]
        EscuelaDeCadetes,

        [Display(Name = "Seguridad del Bombero")]
        SeguridadDelBombero,

        [Display(Name = "Incendios en Vehículos")]
        IncendiosVehiculares,

        [Display(Name = "Incendios en Aeronaves")]
        IncendiosAeronaves,

        [Display(Name = "Incendios en Silos / depósitos")]
        IncendiosSilosDepositos,

        [Display(Name = "Logística y Comunicaciones")]
        LogisticaComunicaciones,

        [Display(Name = "Drones y Tecnología aplicada")]
        DronesTecnologia,

        [Display(Name = "Gestión de Riesgo / Planificación")]
        GestionDeRiesgo,

        [Display(Name = "Primeros Auxilios / RCP")]
        PrimerosAuxiliosRCP,

        [Display(Name = "Tácticas de Ventilación")]
        TacticasVentilacion,

        [Display(Name = "Uso de Equipos de Respiración Autónoma (ERA)")]
        UsoERA,

        [Display(Name = "Capacitación Legal / Normativa")]
        CapacitacionLegalNormativa,

        [Display(Name = "Otro")]
        Otro
    }
}
