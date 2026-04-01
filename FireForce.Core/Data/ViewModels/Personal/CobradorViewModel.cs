using System.ComponentModel.DataAnnotations;
using FireForce.Core.Data.Enums.Personal.Cobrador;
using FireForce.Core.Data.Enums.Socios;
using FireForce.Core.Shared;

namespace FireForce.Core.Data.ViewModels.Personal
{
    /// <summary>
    /// ViewModel para representar un cobrador.
    /// </summary>
    public class CobradorViewModel : PersonalViewModel
    {
        /// <summary>
        /// Estado del cobrador.
        /// </summary>
        [Required(ErrorMessage = "El estado del cobrador es obligatorio. Seleccioná una opción válida.")]
        public EstadoCobrador? Estado { get; set; } = EstadoCobrador.Activo;

        /// <summary>
        /// Regiónes asignadas al cobrador.
        /// </summary>
        [Required(ErrorMessage = "Debés asignar al menos una zona al cobrador.")]
        public Zona ZonasAsignadas { get; set; } = Zona.Ninguna;

        public string ZonasAsignadasAsString
        {
            get
            {
                if (ZonasAsignadas == Zona.Ninguna)
                    return "Ninguna";
                var zonas = Enum.GetValues(typeof(Zona))
                               .Cast<Zona>()
                               .Where(z => z != Zona.Ninguna && ZonasAsignadas.HasFlag(z))
                               .Select(z => z.GetDisplayName());

                return string.Join(", ", zonas);
            }
        }
    }
}
