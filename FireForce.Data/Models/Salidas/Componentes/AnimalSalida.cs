using FireForce.Data.Models.Personas;
using FireForce.Data.Models.Salidas.Planillas;
using FireForce.Shared.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FireForce.Data.Models.Salidas.Componentes
{
    /// <summary>
    /// Representa a un animal damnificado.  
    /// </summary>
    public class AnimalSalida
    {
        /// <summary>
        /// Identificador único del animal afectado.
        /// </summary>
        public int AnimalSalidaId { get; set; }

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
        /// Estado en que se encontró al animal. (Rescatado - Herido - Fallecido - Desaparecido)
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
        /// Nombre del responsable del animal
        /// </summary>
        [StringLength(255)]
        public string? NombreResponsable { get; set; }

        /// <summary>
        /// Apellido del responsable del animal
        /// </summary>
        [StringLength(255)]
        public string? ApellidoResponsable { get; set; }

        /// <summary>
        /// DNI del responsable del animal
        /// </summary>
        [StringLength(255)]
        public string? DocumentoResponsable { get; set; }

        /// <summary>
        /// Identificador único de la salida a la que pertenece el damnificado.
        /// </summary>
        public int SalidaId { get; set; }

        /// <summary>
        /// Referencia a la salida a la que pertenece el damnificado.
        /// </summary>
        [ForeignKey(nameof(SalidaId))]
        public Salida Salida { get; set; } = null!;
    }
}