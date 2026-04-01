using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Vista.Data.Models.Personas;
using Vista.Data.Models.Vehiculos;

namespace Vista.Data.Models.Salidas.Componentes
{
    public class VehiculoAfectado  : Vehiculo
    {
        /// <summary>
        /// Identificador único del seguro asociado al vehículo.
        /// </summary>
        public int? SeguroId { get; set; }

        /// <summary>
        /// Objeto que representa el seguro del vehículo.
        /// </summary>
        [ForeignKey(nameof(SeguroId))]
        public SeguroVehiculo? Seguro { get; set; }

        /// <summary>
        /// Nombre y apellido del dueño del vehículo, con un máximo de 255 caracteres.
        /// </summary>
        [StringLength(255)]
        public string? NombreYApellidoDuenio { get; set; }

        /// <summary>
        /// Color del vehículo, con un máximo de 255 caracteres.
        /// </summary>
        [StringLength(255)]
        public string? Color { get; set; }

        /// <summary>
        /// Airbag del vehículo. Indica si saltaron los airbags en caso de accidente. 
        /// </summary>
        public bool Airbag { get; set; }

        /// <summary>
        /// Observaciones adicionales sobre el vehículo.
        /// </summary>
        public string? Observaciones { get; set; }

        /// <summary>
        /// Indica si se sabe quién era el conductor del vehículo.
        /// </summary>
        public bool SeConoceConductor { get; set; } = false;

        /// <summary>
        /// Todos los ocupantes del vehículo (conductor y pasajeros).
        /// </summary>
        public List<OcupanteVehiculo> Ocupantes { get; set; } = new();
    }
}
