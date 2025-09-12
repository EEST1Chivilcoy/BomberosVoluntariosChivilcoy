using Vista.Data.Models.Vehiculos;

namespace Vista.Data.Models.Salidas.Componentes
{
    public class SeguroVehiculo : Seguro
    {
        /// <summary>
        /// Número de la póliza del seguro.
        /// </summary>
        public string? NumeroDePoliza { get; set; }

        /// <summary>
        /// Fecha de vencimiento de la póliza del seguro.
        /// </summary
        public DateTime? FechaDeVencimineto { get; set; }

        /// <summary>
        /// Relación con el vehículo asegurado.
        /// </summary>
        public VehiculoAfectado Vehiculo { get; set; }
    }
}
