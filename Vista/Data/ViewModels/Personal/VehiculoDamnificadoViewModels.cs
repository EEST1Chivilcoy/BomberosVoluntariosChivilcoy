﻿using System.ComponentModel.DataAnnotations;
using Vista.Data.Models.Personas;

namespace Vista.Data.ViewModels.Personal
{
    public class VehiculoDamnificadoViewModels
    {
        [Required, StringLength(255)]
        public string Marca { get; set; }
        [Required, StringLength(255)]
        public string Modelo { get; set; }
        public int Año { get; set; }
        [Required, StringLength(255)]
        public string Patente { get; set; }
        [Required, StringLength(255)]
        public string Tipo { get; set; }

        [Required, StringLength(255)]
        public string Color { get; set; }
        public bool Airbag { get; set; }
        public string CompañiaAseguradora { get; set; }
        [Required, StringLength(255)]
        public string NumeroDePoliza { get; set; }
        public DateTime FechaDeVencimineto { get; set; }
        public int? DamnificadoId { get; set; }
        public Damnificado_Salida? Damnificado { get; set; }
    }
}
