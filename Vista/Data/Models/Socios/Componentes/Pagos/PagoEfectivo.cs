using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Vista.Data.Models.Socios;

namespace Vista.Data.Models.Socios.Componentes.Pagos
{
    /// <summary>
    /// Pago realizado en efectivo por un socio.
    /// Incluye información del cobrador y si fue entregado a la Comisión Directiva.
    /// </summary>
    public class PagoEfectivo : PagoSocio
    {
        /// <summary>
        /// Nombre o identificador del cobrador que recibió el efectivo del socio.
        /// Este es quien realiza la primera validación (cobranza).
        /// </summary>
        [Required]
        [StringLength(255)]
        public string Cobrador { get; set; } = string.Empty;

        /// <summary>
        /// Fecha en que el efectivo fue entregado a la Comisión Directiva.
        /// Null si aún no se entregó.
        /// </summary>
        public DateTime? FechaEntregaAComision { get; set; }
    }
}
