using System.ComponentModel.DataAnnotations;
using Vista.Data.Enums;

namespace Vista.Data.ViewModels.Servicios
{
    public class ServicioEspecialColocaciónDrizaViewModels : ServicioEspecialViewModel
    {
        /// <summary>
        /// Si el establecimiento es educativo, el tipo de establecimiento.
        /// </summary>
        public TipoIncendioEstablecimientoEducativo? EstablecimientoEducativo { get; set; }

        /// <summary>
        /// Si el establecimiento es público, el tipo de establecimiento.
        /// </summary>
        public TipoIncendioEstablecimientoPublico? EstablecimientoPublico { get; set; }

        /// <summary>
        /// Nombre del establecimiento donde se coloco la driza.
        /// </summary>
        [Required, StringLength(255)]
        public string? NombreEstablecimiento { get; set; }

        /// <summary>
        /// Detalles adicionales sobre la colocación de la driza.
        /// </summary>
        [StringLength(255)]
        public string? Detalles { get; set; }
    }
}
