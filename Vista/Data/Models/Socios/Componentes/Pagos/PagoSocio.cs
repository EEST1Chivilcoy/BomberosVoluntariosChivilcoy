using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Vista.Data.Enums.Socios;
using Vista.Data.Models.Personas.Personal;
using Vista.Data.Models.Socios;

namespace Vista.Data.Models.Socios.Componentes.Pagos
{
    /// <summary>
    /// Pago abstracto realizado por un socio.
    /// </summary>
    public abstract class PagoSocio
    {
        [Key]
        public int Id { get; set; }

        public FormaDePago Tipo { get; set; }

        /// <summary>
        /// Fecha en que se registró el pago.
        /// </summary>
        [Required]
        public DateTime Fecha { get; set; } = DateTime.Now;

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

        public Socio Socio { get; set; } = null!;

        /// <summary>
        /// Flag que indica si el pago fue confirmado por la Comisión Directiva.
        /// La doble validación se modela con: cobradoPor (cobrador) y confirmadoPorComision (comisionDirectiva)
        /// </summary>
        public bool ConfirmadoPorComision { get; set; }

        /// <summary>
        /// Miembro de la Comisión Directiva que confirmó el pago.
        /// </summary>
        public ComisionDirectiva? ConfirmadoPor { get; set; }
    }
}
