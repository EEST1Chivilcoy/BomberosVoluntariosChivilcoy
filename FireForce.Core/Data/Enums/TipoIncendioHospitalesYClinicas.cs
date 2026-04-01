using System.ComponentModel.DataAnnotations;

namespace Vista.Data.Enums
{
    public enum TipoIncendioHospitalesYClinicas
    {
        [Display(Name = "Hospital Municipal")]
        HospitalMunicipal,

        [Display(Name = "Clínica Privada (Otra)")]
        ClinicaPrivadaOtra,

        [Display(Name = "CAPS")]
        CAPS,

        [Display(Name = "Salita barrial / Unidad sanitaria")]
        SalitaBarrialUnidadSanitaria,

        [Display(Name = "Laboratorio de análisis clínicos")]
        LaboratorioAnalisisClinicos,

        [Display(Name = "Consultorio médico privado")]
        ConsultorioMedicoPrivado,

        [Display(Name = "Centro de rehabilitación / kinesiología")]
        CentroRehabilitacionKinesiologia,

        [Display(Name = "Otro")]
        Otro
    }
}