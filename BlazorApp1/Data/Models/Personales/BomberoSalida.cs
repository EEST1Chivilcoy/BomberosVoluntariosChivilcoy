﻿using BlazorApp1.Data.Enums;
using BlazorApp1.Data.Models.Salidas.Planillas;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorApp1.Data.Models.Personales
{
    public class BomberoSalida
    {
        public int BomberoSalidaId { get; set; }

        public bool Salio { get; set; }
        public EscalafonJerarquico Grado { get; set; }

        public int PersonaId { get; set; }
        [ForeignKey("PersonaId")]
        public Bombero Bombero { get; set; }

        public int SalidaId { get; set; }
        public Salida Salida { get; set; }
    }
}
