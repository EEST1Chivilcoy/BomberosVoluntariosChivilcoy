using System.ComponentModel.DataAnnotations;

namespace Vista.Data.Enums.Discriminadores
{
    public enum TipoDeEmergencia
    {
        /// <summary>
        /// Accidente
        /// </summary>
        [Display(Name = "Accidente")]
        Accidente = 1,

        /// <summary>
        /// Factor Climático
        /// </summary>
        [Display(Name = "Factor Climático")]
        FactorClimatico = 2,

        /// <summary>
        /// Material Peligroso
        /// </summary>
        [Display(Name = "Material Peligroso")]
        MaterialPeligroso = 3,

        /// <summary>
        /// Servicio Especial Representación
        /// </summary>
        [Display(Name = "Servicio Especial Representación")]
        ServicioEspecialRepresentacion = 4,

        /// <summary>
        /// Rescate Animal
        /// </summary>
        [Display(Name = "Rescate Animal")]
        RescateAnimal = 5,

        /// <summary>
        /// Rescate Persona
        /// </summary>
        [Display(Name = "Rescate Persona")]
        RescatePersona = 6,

        /// <summary>
        /// Incendio Comercio
        /// </summary>
        [Display(Name = "Incendio Comercio")]
        IncendioComercio = 7,

        /// <summary>
        /// Incendio Establecimiento Educativo
        /// </summary>
        [Display(Name = "Incendio Establecimiento Educativo")]
        IncendioEstablecimientoEducativo = 8,

        /// <summary>
        /// Incendio Establecimiento Público
        /// </summary>
        [Display(Name = "Incendio Establecimiento Público")]
        IncendioEstablecimientoPublico = 9,

        /// <summary>
        /// Incendio Forestal
        /// </summary>
        [Display(Name = "Incendio Forestal")]
        IncendioForestal = 10,

        /// <summary>
        /// Incendio Hospitales y Clínicas
        /// </summary>
        [Display(Name = "Incendio Hospitales y Clínicas")]
        IncendioHospitalesYClinicas = 11,

        /// <summary>
        /// Incendio Industria
        /// </summary>
        [Display(Name = "Incendio Industria")]
        IncendioIndustria = 12,

        /// <summary>
        /// Incendio Vivienda
        /// </summary>
        [Display(Name = "Incendio Vivienda")]
        IncendioVivienda = 13,

        /// <summary>
        /// Servicio Especial Prevención
        /// </summary>
        [Display(Name = "Servicio Especial Prevención")]
        ServicioEspecialPrevencion = 14,

        /// <summary>
        /// Incendio
        /// </summary>
        [Display(Name = "Incendio")]
        Incendio = 15,

        /// <summary>
        /// Incendio Aeronaves
        /// </summary>
        [Display(Name = "Incendio Aeronaves")]
        IncendioAeronaves = 17,

        /// <summary>
        /// Servicio Especial Capacitación
        /// </summary>
        [Display(Name = "Servicio Especial Capacitación")]
        ServicioEspecialCapacitacion = 18,

        /// <summary>
        /// Servicio Especial Colocación Driza
        /// </summary>
        [Display(Name = "Servicio Especial Colocación Driza")]
        ServicioEspecialColocacionDriza = 19,

        /// <summary>
        /// Servicio Especial Suministro Agua
        /// </summary>
        [Display(Name = "Servicio Especial Suministro Agua")]
        ServicioEspecialSuministroAgua = 20,

        /// <summary>
        /// Servicio Especial Falsa Alarma
        /// </summary>
        [Display(Name = "Servicio Especial Falsa Alarma")]
        ServicioEspecialFalsaAlarma = 21,

        /// <summary>
        /// Servicio Especial Retirado de Obito
        /// </summary>
        [Display(Name = "Servicio Especial Retirado de Obito")]
        ServicioEspecialRetiradoDeObito = 22,

        /// <summary>
        /// Servicio Especial Colaboración Fuerzas Seguridad
        /// </summary>
        [Display(Name = "Servicio Especial Colaboración Fuerzas Seguridad")]
        ServicioEspecialColaboracionFuerzasSeguridad = 23
    }
}
