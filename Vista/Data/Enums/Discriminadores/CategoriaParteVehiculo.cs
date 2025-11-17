using System.ComponentModel.DataAnnotations;

namespace Vista.Data.Enums.Discriminadores
{
    /// <summary>
    /// Categoría de parte de vehículo.
    /// </summary>
    public enum CategoriaParteVehiculo
    {
        /// <summary>
        /// Parte correspondiente a una embarcación.
        /// </summary>
        [Display(Name = "Embarcación")]
        Embarcacion = 1,

        /// <summary>
        /// Parte correspondiente a un móvil.
        /// </summary>
        [Display(Name = "Móvil")]
        Movil = 2
    }
}
