﻿using Vista.Data.Models.Personales;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Vista.Data.Enums;

namespace Vista.Data.Models.Salidas.Componentes
{
    public class Incidente
    {
        public int IncidenteId { get; set; }
        public DateTime Fecha { get; set; }
        [Required, StringLength(255)]
        public string? Observacion { get; set; }
        public TipoIncidente Tipo{get; set;}
        public int? PersonaId { get; set; }
        [ForeignKey(nameof(PersonaId))]
        public Bombero? Encargado { get; set; }
        public int? VehiculoId { get; set; }
        [ForeignKey(nameof(VehiculoId))]
        public VehiculoSalida? Vehiculo { get; set; }
        public int? DependenciaId { get; set; }
        [ForeignKey(nameof(DependenciaId))]
        public IncidenteDependencia? Dependencia { get; set; }
    }
}
