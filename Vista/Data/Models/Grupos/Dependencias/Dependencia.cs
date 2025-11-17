using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Vista.Data.Models.Personas.Personal;

namespace Vista.Data.Models.Grupos.Dependencias
{
    /// <summary>
    /// Clase que representa una dependencia de bomberos.
    /// </summary>
    public class Dependencia
    {
        /// <summary>
        /// Identificador único de la dependencia.
        /// </summary>
        public int DependenciaId { get; set; }

        /// <summary>
        /// Nombre de la dependencia. Campo obligatorio.
        /// </summary>
        [Required, StringLength(255)]
        public string NombreDependencia { get; set; } = null!;

        /// <summary>
        /// Encargado de la dependencia. Campo obligatorio.
        /// </summary>
        public Bombero Encargado { get; set; } = null!;

        /// <summary>
        /// Lista de bomberos que pertenecen a la dependencia.
        /// </summary>
        public List<Bombero_Dependencia> Bomberos { get; set; } = new();
    }
}
