﻿using System.ComponentModel.DataAnnotations.Schema;
using Vista.Data.Models.Salidas.Componentes;
using Vista.Data.Models.Salidas.Planillas;

namespace Vista.Data.Models.Personales
{
    public class FuerzaInterviniente
    {
        public int Id { get; set; }
        // Relación con Salida
        public int SalidaId { get; set; }

        [ForeignKey(nameof(SalidaId))]
        public Salida Salida { get; set; } = null!;
        public int FuerzaId { get; set; }

        [ForeignKey(nameof(FuerzaId))]
        public Fuerza Fuerza { get; set; } = null!;
        public string? Encargado { get; set; }
        public Vehiculo Vehiculo { get; set; } = null!;
        public Damnificado? Damnificado { get; set; }
        public string? Destino { get; set; }
    }
}
