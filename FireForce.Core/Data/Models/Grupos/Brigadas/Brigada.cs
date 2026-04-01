using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Vista.Data.Models.Personas.Personal;

namespace Vista.Data.Models.Grupos.Brigadas
{
    /// <summary>
    /// Clase que representa una brigada de bomberos.
    /// </summary>
    public class Brigada
    {
        /// <summary>
        /// Identificador único de la brigada
        /// </summary>
        public int BrigadaId { get; set; }

        /// <summary>
        /// Nombre de la brigada. Campo obligatorio.
        /// </summary>
        [Required, StringLength(255)]
        public string NombreBrigada { get; set; } = null!;

        /// <summary>
        /// Bomberos que pertenecen a la brigada.
        /// </summary>
        public List<Bombero_Brigada> Bomberos { get; set; } = new List<Bombero_Brigada>();

        /// <summary>
        /// Encargado de la brigada. Campo obligatorio.
        /// </summary>
        public Bombero Encargado { get; set; } = null!;
    }
}
