﻿using System.ComponentModel.DataAnnotations;

namespace Vista.Data.Enums.Discriminadores
{
    public enum TipoPersona
    {
        /// <summary>
        /// Representa a un bombero.
        /// </summary>
        [Display(Name = "Bombero")]
        Bombero = 1,

        /// <summary>
        /// Representa a un miembro de la Comisión Directiva.
        /// </summary>
        [Display(Name = "Comisión Directiva")]
        ComisionDirectiva = 2,

        /// <summary>
        /// Representa a una persona damnificada.
        /// </summary>
        [Display(Name = "Damnificado")]
        Damnificado = 3
    }
}