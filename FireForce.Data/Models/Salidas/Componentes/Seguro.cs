using FireForce.Core.Data.Models.Salidas.Planillas;
using System.ComponentModel.DataAnnotations;
using FireForce.Core.Data.Enums.Discriminadores;

namespace FireForce.Core.Data.Models.Salidas.Componentes
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
    }
}
