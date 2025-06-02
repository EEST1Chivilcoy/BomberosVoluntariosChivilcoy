﻿using System.ComponentModel.DataAnnotations;

namespace Vista.Data.Enums.Salidas
{
    /// <summary>
    /// Tipos de Rescate
    /// </summary>
    public enum RescateTipo
    {
        /// <summary>
        /// Rescate de Persona
        /// </summary>
        [Display(Name = "Persona")]
        Persona,

        /// <summary>
        /// Rescate de Animal
        /// </summary>
        [Display(Name = "Animal")]
        Animal
    }
}
