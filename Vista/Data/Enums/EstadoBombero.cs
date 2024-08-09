﻿namespace Vista.Data.Enums;
using System.ComponentModel.DataAnnotations;


    public enum EstadoBombero
    {
        [Display(Name = "Cuerpo Activo")]
        CuerpoActivo,
        [Display(Name = "Reserva")]
        Reserva,
        [Display(Name = "Comision Directiva")]
        ComisionDirectiva,
        [Display(Name = "Inactivo")]
        Inactivo,
    }
