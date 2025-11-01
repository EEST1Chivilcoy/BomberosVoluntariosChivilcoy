﻿using Vista.Data.Enums;

namespace Vista.Data.ViewModels.FactorClimatico
{
    public class FactorClimaticoViewModel : SalidasViewModels
    {
        public TipoFactoresClimaticos Tipo { get; set; }
        public TipoEvacuacion Evacuacion { get; set; }
        public TipoSuperficie Superficie { get; set; }
        public int CantidadAfectada { get; set; }
    }
}
