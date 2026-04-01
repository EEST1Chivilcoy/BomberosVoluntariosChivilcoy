﻿using System.ComponentModel.DataAnnotations;
using Vista.Data.Enums;

namespace Vista.Data.ViewModels.Servicios
{
    public class ServicioEspecialRepresentacionViewModels : ServicioEspecialViewModel
    {
        [Required]
        public TipoServicioRepresentaciones TipoServicioRepresentacion { get; set; }

        [StringLength(255)]
        public string? OtroRepresentacion { get; set; }

        [Required]
        public TipoOrganizacionBeneficiada TipoOrganizacion { get; set; }

        [StringLength(255)]
        public string? OtraOrganizacion { get; set; }
    }
}
