using System.ComponentModel.DataAnnotations;

namespace Vista.Data.Enums.Discriminadores
{
    /// <summary>
    /// Representa los tipos de historial de socio en el sistema.
    /// </summary>
    public enum TipoHistorialSocio
    {
        /// <summary>
        /// Tipo de historial que representa los cambios en el estado del socio.
        /// </summary>
        [Display(Name = "Historial de Estado")]
        HistorialDeEstado = 1,

        /// <summary>
        /// Tipo de historial que representa los pagos realizados por el socio.
        /// </summary>
        [Display(Name = "Historial de Pago")]
        HistorialDePago = 2,

        /// <summary>
        /// Tipo de historial que representa los cambios en la cuota del socio.
        /// </summary>
        [Display(Name = "Historial de Cuota")]
        HistorialDeCuota = 3
    }
}
