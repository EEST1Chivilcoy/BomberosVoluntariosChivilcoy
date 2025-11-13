using System.ComponentModel.DataAnnotations;
using Vista.Data.Enums.Socios;
using Vista.Data.Models.Imagenes;

namespace Vista.Data.Models.Socios.Componentes.Pagos
{
    public class PagoTransferencia : PagoElectronico
    {
        /// <summary>
        /// Representa el banco desde el cual se realizó la transferencia.
        /// </summary>
        [Required]
        public BancosConocidos BancoOrigen { get; set; }

        /// <summary>
        /// Representa el nombre del banco de origen si no está en la lista de bancos conocidos.
        /// </summary>
        public string? OtroBancoOrigen { get; set; }

        /// <summary>
        /// Representa la imagen del comprobante de la transferencia.
        /// </summary>
        public Comprobante Comprobante { get; set; } = null!;
    }
}
