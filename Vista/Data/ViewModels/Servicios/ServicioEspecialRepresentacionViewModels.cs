﻿using System.ComponentModel.DataAnnotations;
using Vista.Data.Enums;

namespace Vista.Data.ViewModels.Servicios
{
    public class ServicioEspecialRepresentacionViewModels : SalidasViewModels
    {
        public TipoServiciosEspeciales Tipo { get; set; }
        public TipoServicioRepresentacion TipoRepresentacion { get; set; }
        public string? OtroRepresentacion { get; set; }
        //Datos de la capacitacion
        public string? NivelCapacitacion { get; set; }
        [StringLength(255)]
        public string? NivelCapacitacionOtro { get; set; }
        public string? TipoCapacitacion { get; set; }
        [StringLength(255)]
        public string? CapacitacionOtra { get; set; }
        [Required, StringLength(255)]
        public string DiasCapacitacion { get; set; }
        [Required, StringLength(255)]
        public string HorariosCapacitacion { get; set; }
        public TipoOrganizacionBeneficiada TipoOrganizacion { get; set; }
    }
}
