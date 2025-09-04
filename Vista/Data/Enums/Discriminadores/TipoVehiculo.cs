using System.ComponentModel.DataAnnotations;

namespace Vista.Data.Enums.Discriminadores
{
    public enum TipoVehiculo
    {
        /// <summary>
        /// Vehículo afectado por un accidente.
        /// </summary>
        [Display(Name = "Vehículo Afectado por Accidente")]
        VehiculoAfectadoAccidente = 1,

        /// <summary>
        /// Vehículo afectado por un incendio.
        /// </summary>
        [Display(Name = "Vehículo Afectado por Incendio")]
        VehiculoAfectadoIncendio = 2,

        /// <summary>
        /// Vehículo perteneciente a un damnificado.
        /// </summary>
        [Display(Name = "Vehículo Damnificado")]
        VehiculoDamnificado = 3,

        /// <summary>
        /// Vehículo asociado a personal.
        /// </summary>
        [Display(Name = "Vehículo de Personal")]
        VehiculoPersonal = 4,

        /// <summary>
        /// Vehículo móvil utilizado en situaciones de emergencia.
        /// </summary>
        [Display(Name = "Móvil")]
        Movil = 5,

        /// <summary>
        /// Vehículo afectado en una situación sin especificar.
        /// </summary>
        [Display(Name = "Vehículo Afectado")]
        VehiculoAfectado = 6,

        /// <summary>
        /// Embarcación asociada a un incidente o situación.
        /// </summary>
        [Display(Name = "Embarcación")]
        Embarcacion = 7
    }
}
