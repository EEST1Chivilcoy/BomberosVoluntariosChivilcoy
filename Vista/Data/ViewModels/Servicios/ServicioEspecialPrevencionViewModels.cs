﻿using System.ComponentModel.DataAnnotations;
using Vista.Data.Enums;
namespace Vista.Data.ViewModels.Servicios
{
    public class ServicioEspecialPrevencionViewModels : ServicioEspecialViewModel
    {
        [StringLength(255)]
        public TipoOrganizacionBeneficiada TipoOrganizacion { get; set; }
        [StringLength(255)]
        public TipoServicioPrevencion TipoPrevencion { get; set; }
    }
}
