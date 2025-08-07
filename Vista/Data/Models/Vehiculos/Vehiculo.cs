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

        /// <summary>
        /// Color del vehículo.
        /// </summary>
        public string? Color { get; set; }
        /// <summary>
        /// Tipo de combustible (por ejemplo, Diesel). Con un maximo de 255 caracteres.
        /// </summary>
        [StringLength(255)]
        public string? Combustible { get; set; }

        /// <summary>
        /// Fecha de realizacion del ultimo service al vehiculo.
        /// </summary>
        public DateTime? FechaUltimoService { get; set; }

        /// <summary>
        /// Fecha en la que se realizara un service al vehiculo
        /// </summary>
        public DateTime? FechaProximoService { get; set; }
        /// <summary>
        /// Identificador único del seguro asociado al vehículo.
        /// </summary>
        public int? SeguroId { get; set; }

        /// <summary>
        /// Objeto que representa el seguro del vehículo.
        /// </summary>
        public SeguroVehiculo? Seguro { get; set; }
    }
}
