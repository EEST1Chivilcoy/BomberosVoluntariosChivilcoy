using System.ComponentModel.DataAnnotations;
using FireForce.Data.Models.Personas.Personal;

namespace FireForce.Data.Models.Grupos.Guardia
{
    /// <summary>
    /// Clase que representa una brigada de bomberos.
    /// </summary>
    public class Guardia
    {
        /// <summary>
        /// Identificador único de la brigada
        /// </summary>
        public int GuardiaId { get; set; }

        /// <summary>
        /// Nombre de la brigada. Campo obligatorio.
        /// </summary>
        [Required, StringLength(255)]
        public string NombreGuardia { get; set; } = null!;

        /// <summary>
        /// Bomberos que pertenecen a la brigada.
        /// </summary>
        public List<Bombero_Guardia> Bomberos { get; set; } = new List<Bombero_Guardia>();

        /// <summary>
        /// Encargado de la brigada. Campo obligatorio.
        /// </summary>
        public Bombero Encargado { get; set; } = null!;
    }
}
