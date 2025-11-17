using System.ComponentModel.DataAnnotations;
using Vista.Data.Enums.Personal.ComisionDirectiva;

namespace Vista.Data.ViewModels.Personal
{
    /// <summary>
    /// ViewModel para representar a un miembro de la Comisión Directiva de la Asociación de Bomberos Voluntarios de Chivilcoy.
    /// </summary>
    public class ComisionDirectivaViewModel : PersonalViewModel
    {
        /// <summary>
        /// Grado del miembro de la Comisión Directiva.
        /// </summary>
        [Required(ErrorMessage = "El grado del miembro de la Comisión Directiva es obligatorio.")]
        public GradoComisionDirectiva? Grado { get; set; }

        /// <summary>
        /// Estado del miembro de la Comisión Directiva.
        /// </summary>
        [Required(ErrorMessage = "El estado del miembro de la Comisión Directiva es obligatorio.")]
        public EstadoComisionDirectiva? Estado { get; set; } = EstadoComisionDirectiva.Activo;
    }
}
