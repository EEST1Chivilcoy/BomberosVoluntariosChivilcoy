using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using Vista.Data.Models.Vehiculos.Flota;

namespace Vista.Data.Models.Imagenes
{
    /// <summary>
    /// Representa una imagen asociada a un vehículo de la flota.
    /// </summary>
    [Index(nameof(VehiculoId), IsUnique = true)]
    public class Imagen_VehiculoSalida : Imagen
    {
        /// <summary>
        /// Identificador único del vehículo de salida al que está asociada la imagen.
        /// </summary>
        public int VehiculoId { get; set; }

        /// <summary>
        /// Relación con la entidad `VehiculoSalida`, que representa el vehículo asociado a esta imagen.
        /// </summary>
        [ForeignKey("VehiculoId")]
        public VehiculoSalida Vehiculo { get; set; } = null!;
    }
}
