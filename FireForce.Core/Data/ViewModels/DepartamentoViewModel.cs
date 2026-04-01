using System.ComponentModel.DataAnnotations;

namespace FireForce.Core.Data.ViewModels
{
    /// <summary>
    /// ViewModel para representar información de un departamento o unidad en un edificio.
    /// </summary>
    public class DepartamentoViewModel
    {
        /// <summary>
        /// Identificador único del departamento.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Número de piso donde se encuentra el departamento.
        /// </summary>
        [StringLength(50)]
        public string? PisoNumero { get; set; }

        /// <summary>
        /// Número o letra del departamento.
        /// </summary>
        [StringLength(50)]
        public string? Depto { get; set; }
    }
}
