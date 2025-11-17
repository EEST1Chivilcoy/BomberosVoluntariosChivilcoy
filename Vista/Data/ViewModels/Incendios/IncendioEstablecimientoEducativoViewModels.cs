using System.ComponentModel.DataAnnotations;
using Vista.Data.Enums;

namespace Vista.Data.ViewModels.Incendios
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
