using FireForce.Data.Models.Personas;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Text;

namespace FireForce.Data.Models.Salidas.Componentes
{
    public class Animal
    {
        /// <summary>
        /// Identificador único del animal afectado.
        /// </summary>
        public int? AnimalId { get; set; }

        /// <summary>
        /// Especie o raza del animal.
        /// </summary>
        [StringLength(255)]
        public string Tipo { get; set; }

        /// <summary>
        /// Estado en que se encontró al animal. (Rescatado - Herido - Muerto - Desaparecido)
        /// </summary>
        [StringLength(255)]
        public string Estado { get; set; }

        /// <summary>
        /// Cantidad del mismo animal que se registró.
        /// </summary>
        public int Cantidad { get; set; }

        /// <summary>
        /// Booleano para saber si se conoce al responsable del animal.
        /// </summary>
        public bool SeConoceResponsable { get; set; }

        /// <summary>
        /// Identificador único del responsable del animal.
        /// </summary>
        [ForeignKey(nameof(Persona.PersonaId))]
        public int ResposableId { get; set; }
    }
}