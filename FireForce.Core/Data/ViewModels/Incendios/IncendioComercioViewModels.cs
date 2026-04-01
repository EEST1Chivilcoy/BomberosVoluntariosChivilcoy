using System.ComponentModel.DataAnnotations;
using Vista.Data.Enums;

namespace Vista.Data.ViewModels.Incendios
{
    public class IncendioComercioViewModels : IncendioViewModels
    {
        /// <summary>
        /// Tipo de comercio donde ocurrió el incendio.
        /// </summary>
        [Required]
        public TipoLugarComercioIncendio TipoLugar { get; set; }
    }
}
