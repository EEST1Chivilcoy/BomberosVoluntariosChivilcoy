﻿using BlazorApp1.Data.Enums;

namespace BlazorApp1.Data.Models.Salidas
{
    public class DatosCapacitacion
    {
        public int DatosCapacitacionId { get; set; }

        public TipoNivelCapacitacion? NivelCapacitacion { get; set; }
        public string? NivelCapacitacionOtro { get; set; }
        public TipoCapacitacion? TipoCapacitacion { get; set; }
        public string? CapacitacionOtra { get; set; }
        public string DiasCapacitacion { get; set; }
        public string HorariosCapacitacion { get; set; }

        public int SalidaId { get; set; }
        public ServicioEspecial ServicioEspecial { get; set; }
    }
}
