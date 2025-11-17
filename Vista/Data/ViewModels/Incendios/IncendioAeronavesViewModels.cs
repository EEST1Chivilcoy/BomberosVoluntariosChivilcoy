using System.ComponentModel.DataAnnotations;
using Vista.Data.Enums;
namespace Vista.Data.ViewModels.Incendios
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