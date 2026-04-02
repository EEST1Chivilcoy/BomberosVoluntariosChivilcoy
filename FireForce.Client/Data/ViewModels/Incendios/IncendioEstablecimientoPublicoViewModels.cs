using System.ComponentModel.DataAnnotations;
using FireForce.Shared.Enums;

namespace FireForce.Client.Data.ViewModels.Incendios
{
    public class IncendioEstablecimientoPublicoViewModels : IncendioViewModels
    {
        /// <summary>
        /// Tipo de establecimiento público donde ocurrió el incendio.
        /// </summary>
        [Required]
        public TipoIncendioEstablecimientoPublico? TipoLugar { get; set; }
    }
}
