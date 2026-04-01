using System.ComponentModel.DataAnnotations;
namespace Vista.Data.Enums
{
    public enum TipoSalida
    {
        [Display(Name = "Accidente")]
        Accidente,
        [Display(Name = "Factor Climático")]
        FactorClimatico,
        [Display(Name = "Material Peligroso")]
        MaterialPeligroso,
        [Display(Name = "Servicio Especial Represetanciones")]
        ServicioEspecialRepresentaciones,
        [Display(Name = "Rescate Animal")]
        RescateAnimal,
        [Display(Name = "Rescate Persona")]
        RescatePersona,
        [Display(Name = "Incendio Comercio")]
        IncendioComercio,
        [Display(Name = "Incendio Establecimiento Educativo")]
        IncendioEstablecimientoEducativo,
        [Display(Name = "Incendio Establecimiento Público")]
        IncendioEstablecimientoPublico,
        [Display(Name = "Incendio Forestal")]
        IncendioForestal,
        [Display(Name = "Incendio Hospitales y Clínicas")]
        IncendioHospitalesYClinicas,
        [Display(Name = "Incendio Industria")]
        IncendioIndustria,
        [Display(Name = "Incendio Vivienda")]
        IncendioVivienda,
        [Display(Name = "Servicio Especial Prevención")]
        ServicioEspecialPrevencion,
        [Display(Name = "Incendio")]
        Incendio,
        [Display(Name = "Servicio Especial")]
        ServicioEspecial

    }
}
