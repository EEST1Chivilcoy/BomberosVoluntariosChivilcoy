using System.ComponentModel.DataAnnotations;

namespace Vista.Data.Enums.Discriminadores
{
    /// <summary>
    /// Representa los tipos de partes de vehículos.
    /// </summary>
    public enum TipoParteVehiculo
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
