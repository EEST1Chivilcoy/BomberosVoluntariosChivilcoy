﻿using System.ComponentModel.DataAnnotations;
using Vista.Data.Enums;

namespace Vista.Data.ViewModels.Rescates
{
    public class RescateAnimaViewModels : SalidasViewModels
    {
        public TipoRescateAnimal TipoRescateAnimal { get; set; }

        [Required, StringLength(255)]
        public string Otro { get; set; }
        public TipoLugarAmbulancias tipoLugarAmbulancias { get; set; }
    }
}
