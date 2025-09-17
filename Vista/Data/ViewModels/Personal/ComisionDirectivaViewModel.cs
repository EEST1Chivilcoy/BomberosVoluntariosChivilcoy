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
        public GradoComisionDirectiva Grado { get; set; }

        /// <summary>
        /// Estado del miembro de la Comisión Directiva.
        /// </summary>
        public EstadoComisionDirectiva Estado { get; set; }
    }
}
