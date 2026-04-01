using System.ComponentModel.DataAnnotations;

namespace Vista.Data.Enums
{
    public enum TipoIncendioEstablecimientoPublico
    {
        [Display(Name = "Oficina Pública")]
        OficinaPublica,

        [Display(Name = "Oficina de Tránsito / Licencias")]
        OficinaTransitoLicencias,

        [Display(Name = "Municipalidad / Intendencia")]
        MunicipalidadIntendencia,

        [Display(Name = "Concejo Deliberante")]
        ConcejoDeliberante,

        [Display(Name = "Registro Civil")]
        RegistroCivil,

        [Display(Name = "ANSES / PAMI / AFIP")]
        OrganismoPrevisionalFiscal,

        [Display(Name = "Comisaría / Dependencia Policial")]
        ComisariaDependenciaPolicial,

        [Display(Name = "Juzgado / Fiscalía")]
        JuzgadoFiscalia,

        [Display(Name = "Biblioteca Pública")]
        BibliotecaPublica,

        [Display(Name = "Centro Cultural / Espacio INCAA")]
        CentroCulturalEspacioINCAA,

        [Display(Name = "Correo Argentino")]
        CorreosEncomiendas,

        [Display(Name = "Terminal de Ómnibus / Estación Ferroviaria")]
        TerminalEstacionTransporte,

        [Display(Name = "Otro")]
        Otro
    }
}