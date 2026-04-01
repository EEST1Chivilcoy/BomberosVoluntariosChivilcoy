using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FireForce.Core.Data.Enums.Discriminadores;
using FireForce.Core.Data.Enums.Socios;
using FireForce.Core.Data.Models.Personas.Personal;
using FireForce.Core.Data.Models.Socios;

namespace FireForce.Core.Data.Models.Socios.Componentes.Pagos
{
    /// <summary>
    /// Pago abstracto realizado por un socio.
    /// </summary>
    public abstract class PagoSocio
    {
        /// <summary>
        /// Identificador único del pago.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Tipo de pago realizado.
        /// </summary>
        public TipoPagoSocio Tipo { get; set; }

        /// <summary>
        /// Fecha en la que se va a cobrar el pago.
        /// </summary>
        [Required]
        public DateTime Fecha { get; set; } = DateTime.Now;

        /// <summary>
        /// Fecha en que el pago fue confirmado o rechazado.
        /// </summary>
        public DateTime? FechaConfirmadoORechazado { get; set; }

        /// <summary>
        /// Razón del rechazo del pago, si fue rechazado.
        /// </summary>
        public string? RazonRechazo { get; set; }

        /// <summary>
        /// Monto pagado.
        /// </summary>
        [Required]
        public double Monto { get; set; }

        /// <summary>
        /// Referencia al socio que realizó el pago.
        /// </summary>
        [Required]
        public int SocioId { get; set; }

        /// <summary>
        /// Estado del pago.
        /// </summary>
        public EstadoPago Estado { get; set; } = EstadoPago.PendienteAConfirmar;

        /// <summary>
        /// Socio que realizó el pago.
        /// </summary>
        public Socio Socio { get; set; } = null!;

        /// <summary>
        /// Miembro de la Comisión Directiva que confirmó el pago.
        /// Se coloca como nullable para permitir pagos pendientes de confirmación.
        /// o por que tambien puede que sea automatica la confirmacion y no haya nadie que lo confirme.
        /// por ejemplo pagos con tarjeta.
        /// </summary>
        public ComisionDirectiva? ConfirmadoPor { get; set; }
    }
}
