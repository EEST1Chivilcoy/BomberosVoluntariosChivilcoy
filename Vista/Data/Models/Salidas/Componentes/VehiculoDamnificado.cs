﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Vista.Data.Models.Personas;
using Vista.Data.Models.Vehiculos;

namespace Vista.Data.Models.Salidas.Componentes
{
    public class VehiculoDamnificado : Vehiculo
    {
        public string Color { get; set; }
        public bool Airbag { get; set; }

        public int? DamnificadoId { get; set; }
        public Damnificado_Salida? Damnificado { get; set; }
    }
}
