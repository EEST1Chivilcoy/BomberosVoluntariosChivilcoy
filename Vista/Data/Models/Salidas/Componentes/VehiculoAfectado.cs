using Vista.Data.Models.Vehiculos;

namespace Vista.Data.Models.Salidas.Componentes
{
    public class VehiculoAfectado : Vehiculo
    {
        /// <summary>
        /// Color del vehículo.
        /// </summary>
        public string? Color { get; set; }

        /// <summary>
        /// Airbag del vehículo.
        /// </summary>
        public bool Airbag { get; set; }

        /// <summary>
        /// Relaciona el vehículo afectado con una salida.
        /// </summary>
        public int SalidaId { get; set; }
    }
}
