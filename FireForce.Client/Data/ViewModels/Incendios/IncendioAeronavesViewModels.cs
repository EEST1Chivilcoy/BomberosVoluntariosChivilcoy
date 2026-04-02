using System.ComponentModel.DataAnnotations;
using FireForce.Core.Data.Enums;
namespace FireForce.Core.Data.ViewModels.Incendios
{
    public class IncendioAeronavesViewModels : IncendioViewModels
    {
        /// <summary>
        /// Tipo de aeronave involucrada en el incendio.
        /// </summary>
        [Required]
        public TipoIncendioAeronaves TipoAeronave { get; set; }
    }
}