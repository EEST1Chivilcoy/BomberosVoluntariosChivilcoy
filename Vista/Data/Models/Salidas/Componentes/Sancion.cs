﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Vista.Data.Enums;
using Vista.Data.Models.Personales;

namespace Vista.Data.Models.Salidas.Componentes
{
    public class Sancion
    {
        public int SancionId { get; set; }

        public DateTime FechaDesde { get; set; }
        public DateTime FechaHasta { get; set; }

        public int PersonaId { get; set; }
        [ForeignKey ("PersonaId")]
       
        public Bombero PersonalSancionado { get; set; }

        public TipoSancion TipoSancion { get; set; }

        public AreaSancion SacionArea { get; set; }
        public int EncargadoAreaId { get; set; }
        [ForeignKey("EncargadoAreaId")]
        public Bombero EncargadoArea { get; set; }
        [StringLength(255)]
        public string? observaciones { get; set; }

    }
}
