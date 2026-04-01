using System.ComponentModel.DataAnnotations;

namespace Vista.Data.Enums.Discriminadores
{
    public enum TipoSeguro
    {
        /// <summary>
        /// Seguro asociado a una salida.
        /// </summary>
        [Display(Name = "Seguro de Salida")]
        SeguroSalida = 1,

        /// <summary>
        /// Seguro asociado a un vehículo.
        /// </summary>
        [Display(Name = "Seguro de Vehículo")]
        SeguroVehiculo = 2
    }
}
