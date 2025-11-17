using System.ComponentModel.DataAnnotations;

namespace Vista.Data.Enums
{
    public enum TipoOrganizacionBeneficiada
    {
        [Display(Name = "Fuerzas de seguridad (Policía, Gendarmería, Defensa Civil)")]
        FuerzasDeSeguridad,

        [Display(Name = "Entidades gubernamentales (Municipio, Provincia, Nación)")]
        EntidadesGubernamentales,

        [Display(Name = "Instituciones educativas (Escuelas, CUCH, Centros de formación)")]
        InstitucionesEducativas,

        [Display(Name = "Instituciones de salud (Hospital, CAPS, clínicas)")]
        InstitucionesDeSalud,

        [Display(Name = "Organizaciones sociales / comunitarias")]
        OrganizacionesSociales,

        [Display(Name = "ONG / Fundación / Asociación civil")]
        ONGFundacion,

        [Display(Name = "Empresa privada / industria local")]
        EmpresaPrivada,

        [Display(Name = "Cooperativa / Mutual / Club")]
        CooperativaMutualClub,

        [Display(Name = "Otra")]
        Otra
    }
}