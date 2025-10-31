﻿using System.ComponentModel.DataAnnotations;
using Vista.Data.Enums;

namespace Vista.Data.ViewModels.Servicios
{
    public class ServicioEspecialCapacitacionViewModel : ServicioEspecialViewModel
    {
        public TipoNivelCapacitacion NivelDeCapacitacion { get; set; }
        public TipoCapacitacion TipoCapacitacion { get; set; }
        public DateTime? DiaHora { get; set; }
        [Required,StringLength(255)]
        public string TipoCapacitacionOtra { get; set; }
        [Required,StringLength(255)]
        public string NivelDeCapacitacionOtro { get; set; }
    }
}
