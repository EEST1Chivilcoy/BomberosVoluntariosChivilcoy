using System.ComponentModel.DataAnnotations;
using FireForce.Core.Data.Enums.Discriminadores;

namespace FireForce.Core.Data.Models.Otros.Partes
{
    /// <summary>
    /// Representa una parte del vehículo.
    /// </summary>
    public class ParteVehiculo
    {
        /// <summary>
        /// Identificador único del parte del vehículo.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Representa el tipo de parte de vehículo. (Embarcación o Movil)
        /// </summary>
        [Required]
        public CategoriaParteVehiculo Tipo { get; set; }

        /// <summary>
        /// Nombre de la parte del vehículo.
        /// </summary>
        [Required]
        public string Nombre { get; set; } = null!;
    }
}
