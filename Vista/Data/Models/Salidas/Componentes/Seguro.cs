﻿using Vista.Data.Models.Salidas.Planillas;
using System.ComponentModel.DataAnnotations;
using Vista.Data.Enums.Discriminadores;

namespace Vista.Data.Models.Salidas.Componentes
{
    public abstract class Seguro
    {
        /// <summary>
        /// Identificador único del seguro.
        /// </summary>
        public int SeguroId { get; set; }

        /// <summary>
        /// Tipo de seguro (Salida o Vehículo).
        /// </summary>
        public TipoSeguro Tipo { get; set; }

        /// <summary>
        /// Nombre de la compañía aseguradora.
        /// </summary>
        public string? CompañiaAseguradora { get; set; }

        /// <summary>
        /// Número de la póliza del seguro.
        /// </summary>
        public string? NumeroDePoliza { get; set; }

        /// <summary>
        /// Fecha de vencimiento de la póliza del seguro.
        /// </summary
        public DateTime? FechaDeVencimineto { get; set; }
    }
}
