﻿using System.ComponentModel.DataAnnotations;
using Vista.Data.Enums;

namespace Vista.Data.ViewModels.Servicios
{
    public class ServicioEspecialCapacitacionViewModel : ServicioEspecialViewModel
    {
        /// <summary>
        /// Tipo de nivel de capacitación.
        /// </summary>
        [Required]
        public TipoNivelCapacitacion NivelDeCapacitacion { get; set; }

        /// <summary>
        /// Tipo de capacitación.
        /// </summary>
        [Required]
        public TipoCapacitacion TipoCapacitacion { get; set; }

        /// <summary>
        /// Día y hora de la capacitación.
        /// </summary>
        [Required]
        public DateTime? DiaHora { get; set; }

        /// <summary>
        /// Duración de la capacitación.
        /// </summary>
        public TimeOnly? Duracion { get; set; }

        /// <summary>
        /// Otra tipo de capacitación, si aplica.
        /// </summary>
        [StringLength(255)]
        public string? TipoCapacitacionOtra { get; set; }

        /// <summary>
        /// Otro nivel de capacitación, si aplica.
        /// </summary>
        [StringLength(255)]
        public string? NivelDeCapacitacionOtro { get; set; }
    }
}
