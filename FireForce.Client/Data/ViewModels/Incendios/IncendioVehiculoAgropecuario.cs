using System.ComponentModel.DataAnnotations;
using FireForce.Core.Data.Enums;

namespace FireForce.Client.Data.ViewModels.Incendios
{
    public class IncendioVehiculoAgropecuario
    {
        /// <summary>
        /// Tipo de máquina agropecuaria involucrada en el incendio.
        /// </summary>
        [Required]
        public TipoMaquinaAgropecuaria? MaquinaAgropecuaria { get; set; }

        /// <summary>
        /// Otro tipo de máquina agropecuaria, si aplica.
        /// </summary>
        [StringLength(255)]
        public string? OtroMaquinaAgropecuaria { get; set; }
    }
}
