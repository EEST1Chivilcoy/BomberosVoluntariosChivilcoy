using System.ComponentModel.DataAnnotations;
using Vista.Data.Enums;

namespace Vista.Data.ViewModels.Incendios
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
