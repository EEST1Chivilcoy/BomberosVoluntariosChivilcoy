using System.ComponentModel.DataAnnotations;

namespace FireForce.Core.Data.ViewModels.Personal
{
    /// <summary>
    /// ViewModel simple para representar una fuerza interviniente con solo Id y Nombre.
    /// </summary>
    public class SimpleFuerzaViewModel
    {
        /// <summary>
        /// Representa el identificador único de la fuerza interviniente.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Representa el nombre de la fuerza interviniente.
        /// </summary>
        [Required]
        public string Nombre { get; set; } = null!;
    }
}
