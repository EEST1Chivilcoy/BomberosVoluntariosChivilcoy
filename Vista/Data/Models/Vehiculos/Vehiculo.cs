using Vista.Data.Models.Salidas.Componentes;
using System.ComponentModel.DataAnnotations;
using Vista.Data.Enums.Discriminadores;

namespace Vista.Data.Models.Vehiculos
{
    /// <summary>
    /// Clase abstracta que representa un vehículo.
    /// </summary>
    public abstract class Vehiculo
    {
        /// <summary>
        /// Identificador único del vehículo.
        /// </summary>
        public int VehiculoId { get; set; }

        /// <summary>
        /// Discriminador que indica el tipo.
        /// </summary>
        public TipoVehiculo Discriminador { get; set; }

        /// <summary>
        /// Marca del vehículo, con un máximo de 255 caracteres.
        /// </summary>
        [StringLength(255)]
        public string? Marca { get; set; }

        /// <summary>
        /// Modelo del vehículo, con un máximo de 255 caracteres.
        /// </summary>
        [StringLength(255)]
        public string? Modelo { get; set; }

        /// <summary>
        /// Año de fabricación del vehículo.
        /// </summary>
        public int? Año { get; set; }

        /// <summary>
        /// Patente del vehículo, con un máximo de 255 caracteres. 
        /// Este campo es obligatorio.
        /// </summary>
        [StringLength(255)]
        [Required]
        public string Patente { get; set; } = null!;

        /// <summary>
        /// Tipo de vehículo (por ejemplo, automóvil, camioneta, etc.), con un máximo de 255 caracteres.
        /// </summary>
        [StringLength(255)]
        public string? Tipo { get; set; }
    }
}
