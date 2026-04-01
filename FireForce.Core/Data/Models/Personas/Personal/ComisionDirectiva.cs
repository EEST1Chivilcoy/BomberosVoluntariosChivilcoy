using System.ComponentModel.DataAnnotations;
using Vista.Data.Enums.Personal.ComisionDirectiva;

namespace Vista.Data.Models.Personas.Personal
{
    /// <summary>
    /// Representa a un miembro de la Comisión Directiva de la Asociación de Bomberos Voluntarios.
    /// </summary>
    public class ComisionDirectiva : Personal
    {
        /// <summary>
        /// Rango Jerárquico del miembro de la Comisión Directiva.
        /// </summary>
        [Required(ErrorMessage = "El grado jerárquico del miembro de la Comisión Directiva es obligatorio.")]
        public GradoComisionDirectiva Grado { get; set; }

        /// <summary>
        /// Estado actual del miembro de la Comisión Directiva.
        /// Puede ser Activo, Baja o Baja por Fallecimiento.
        /// </summary>
        [Required(ErrorMessage = "El estado del miembro de la Comisión Directiva es obligatorio.")]
        public EstadoComisionDirectiva Estado { get; set; }
    }
}
