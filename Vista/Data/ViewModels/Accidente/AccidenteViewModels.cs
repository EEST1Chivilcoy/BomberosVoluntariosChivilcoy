﻿using System.ComponentModel.DataAnnotations;
using Vista.Data.Enums;
using Vista.Data.Models.Salidas.Componentes;

namespace Vista.Data.ViewModels.Accidente
{
    public class AccidenteViewModels : SalidasViewModels
    {
        public TipoAccidente Tipo { get; set; }
        public int CantidadVheiculo { get; set; }
        public List<VehiculoAfectadoAccidente> VehiculosAfectado { get; set; }
        public TipoCondicionesClimaticas CondicionesClimaticas { get; set; }
        [Required, StringLength(255)]
        public string? OtroCondicion { get; set; }
    }
}
