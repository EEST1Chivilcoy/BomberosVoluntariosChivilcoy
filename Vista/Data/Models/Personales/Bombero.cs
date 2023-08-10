﻿using Vista.Data.Enums;
using Vista.Data.Models.Salidas.Componentes;
using Vista.Data.Models.Salidas.Planillas;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vista.Data.Models.Personales
{
    [Index(nameof(NumeroLegajo))]
    [Index(nameof(EquipoId), IsUnique = true)]
    public class Bombero : Persona
    {
        [Required]
        public int NumeroLegajo { get; set; }
        public EstadoBombero Estado { get; set; } 
        public TipoDotaciones Dotacion { get; set; }
        public DateTime FechaAceptacion { get; set; }
        public EscalafonJerarquico Grado { get; set; }
        [StringLength(255)]
        public string? Resolucion1 { get; set; }
        [StringLength(255)]
        public string? Resolucion2 { get; set; }
        [StringLength(255)]
        public string? Resolucion3 { get; set; }
        [StringLength(255)]
        public string? Resolucion4 { get; set; }
        [StringLength(255)]
        public string? Resolucion5 { get; set; }
        [StringLength(255)]
        public string? Resolucion6 { get; set; }
        public bool Chofer { get; set; }
        public DateTime? VencimientoRegistro { get; set; }

        public List<Firma> Firmas { get; set; }

        public int BrigadaId { get; set; }
        public Brigada Brigada { get; set; }
        public int ImagenId { get; set; }
        [ForeignKey("ImagenId")]
        public ImagenBombero Imagen { get; set; }

        public List<VehiculoPersonal> Vehiculos { get; set; } = new();

        public MovilBombero? Movil { get; set; }

        public BomberoDependencia? Dependencia { get; set; }

        public List<Incidente> Incidentes { get; set; } = new();

        public List<BomberoSalida> Salidas { get; set; } = new();
        public int EquipoId { get; set; }
        public Comunicacion? Handie { get; set; }   
    }
}
