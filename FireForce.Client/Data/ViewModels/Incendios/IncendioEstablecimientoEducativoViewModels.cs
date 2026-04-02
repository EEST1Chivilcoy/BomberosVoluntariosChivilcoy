using System.ComponentModel.DataAnnotations;
using FireForce.Core.Data.Enums;

namespace FireForce.Client.Data.ViewModels.Incendios
{
    public class IncendioEstablecimientoEducativoViewModels : IncendioViewModels
    {
        /// <summary>
        /// Tipo de establecimiento educativo donde ocurrió el incendio.
        /// </summary>
        [Required]
        public TipoIncendioEstablecimientoEducativo TipoLugar { get; set; }
    }
}
