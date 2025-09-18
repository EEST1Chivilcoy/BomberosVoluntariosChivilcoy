using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Vista.Data.Models.Personas;
using Vista.Data.Models.Vehiculos;

namespace Vista.Data.Models.Salidas.Componentes
{
    public class VehiculoAfectado  : Vehiculo
    {
        // -- Seguro del vehículo --

        /// <summary>
        /// Identificador único del seguro asociado al vehículo.
        /// </summary>
        public int? SeguroId { get; set; }

        /// <summary>
        /// Objeto que representa el seguro del vehículo.
        /// </summary>
        public SeguroVehiculo? Seguro { get; set; }

        // -- Datos específicos del vehículo damnificado --

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

        // -- Relaciones con damnificados --

        /// <summary>
        /// Indica si se sabe quién era el conductor del vehículo.
        /// </summary>
        public bool seConoceConductor { get; set; } = false;

        /// <summary>
        /// Relación con el damnificado chofer del vehículo.
        /// </summary>
        public Damnificado_Salida? ConductorDamnificado { get; set; } = null!;

        /// <summary>
        /// Damnificados pasajeros del vehículo.
        /// </summary>
        public List<Damnificado_Salida> PasajerosDamnificados { get; set; } = new();
    }
}
