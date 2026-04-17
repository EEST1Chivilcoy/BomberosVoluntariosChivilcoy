using FireForce.Data.Models.Personas;
using FireForce.Shared.Enums;
using System.ComponentModel.DataAnnotations;

namespace FireForce.Data.Models.Salidas.Componentes
{
    public class AnimalSalida
    {
        /// <summary>
        /// Identificador único del animal afectado.
        /// </summary>
        public int AnimalId { get; set; }

        /// <summary>
        /// Especie o raza del animal.
        /// </summary>
        public TipoAnimal Tipo { get; set; }

        /// <summary>
        /// Especie o raza del animal que no se encontraba en el desplegable.
        /// </summary>
        [StringLength(255)]
        public string? TipoOtro { get; set; }

        /// <summary>
        /// Estado en que se encontró al animal. (Rescatado - Herido - Muerto - Desaparecido)
        /// </summary>
        public EstadoAnimal Estado { get; set; }

        /// <summary>
        /// Cantidad del mismo animal que se registró.
        /// </summary>
        public int Cantidad { get; set; }

        /// <summary>
        /// Nombre al que responde el animal.
        /// </summary>
        [StringLength(255)]
        public string? Nombre { get; set; }

        /// <summary>
        /// Observaciones adicionales sobre el animal.
        /// </summary>
        [StringLength(255)]
        public string? Observaciones { get; set; }

        /// <summary>
        /// Identificador único del responsable del animal.
        /// </summary>
        public int? DamnificadoId { get; set; }

        public Damnificado_Salida? Damnificado { get; set; }
    }
}