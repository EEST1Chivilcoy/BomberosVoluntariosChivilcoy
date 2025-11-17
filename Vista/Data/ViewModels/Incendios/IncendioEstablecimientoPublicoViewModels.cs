using System.ComponentModel.DataAnnotations;
using Vista.Data.Enums;

namespace Vista.Data.ViewModels.Incendios
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
