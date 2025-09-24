using Vista.Data.Enums.Personal.Cobrador;
using Vista.Data.Enums.Socios;

namespace Vista.Data.ViewModels.Personal
{
    /// <summary>
    /// ViewModel para representar un cobrador.
    /// </summary>
    public class CobradorViewModel : PersonalViewModel
    {
        /// <summary>
        /// Estado del cobrador.
        /// </summary>
        public EstadoCobrador Estado { get; set; } = EstadoCobrador.Activo;

        /// <summary>
        /// Regiónes asignadas al cobrador.
        /// </summary>
        public Zona ZonasAsignadas { get; set; } = Zona.Ninguna;
    }
}
