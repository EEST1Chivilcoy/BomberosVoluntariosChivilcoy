using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Vista.Data.Models.Socios;

namespace Vista.Data.Models.Socios.Componentes.Pagos
{
    /// <summary>
    /// Implementación de pago electrónico.
    /// Aunque hoy no se use, queda preparado para futuro.
    /// </summary>
    public abstract class PagoElectronico : PagoSocio
    {
        /// <summary>
        /// Identificador de la transacción en el gateway o banco.
        /// </summary>
        [StringLength(255)]
        public string? TransactionId { get; set; }
    }
}
