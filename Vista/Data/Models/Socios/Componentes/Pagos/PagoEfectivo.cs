using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Vista.Data.Models.Personas.Personal;
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
        /// Identificador del cobrador que recibió el efectivo del socio.
        /// Este es quien realiza la primera validación (cobranza).
        /// </summary>
        [Required]
        public int CobradorId { get; set; }

        /// <summary>
        /// Cobrador que recibió el efectivo del socio.
        /// </summary>
        [Required]
        public Cobrador Cobrador { get; set; } = null!;

        /// <summary>
        /// Fecha en que el efectivo fue entregado a la Comisión Directiva.
        /// Null si aún no se entregó.
        /// </summary>
        public DateTime? FechaEntregaAComision { get; set; }
    }
}
