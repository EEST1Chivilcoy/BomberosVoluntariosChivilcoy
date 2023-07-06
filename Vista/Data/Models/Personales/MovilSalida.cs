﻿using Vista.Data.Models.Salidas.Planillas;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vista.Data.Models.Personales
{
    public class MovilSalida
    {
        public int MovilSalidaId { get; set; }

        public bool CargoCombustible { get; set; }
        //deberia ir aca si cargo conbustible 
        public string? NumeroFactura { get; set; }
        public string? FechaFactura { get; set; }
        public string? TipoConbustible { get; set; }
        public string? CantidadLitros { get; set; }
        public string? Usuario { get; set; }
        public string? NombreYApellidoRecpetor { get; set; }
        public string? TelefonoReceptor { get; set; }
        public int PersonaId { get; set; }
        [ForeignKey("PersonaId")]
        public Bombero Chofer { get; set; }

        public int MovilId { get; set; }
        public Movil Movil { get; set; }

        public int SalidaId { get; set; }
        public Salida Salida { get; set; }
    }
}
