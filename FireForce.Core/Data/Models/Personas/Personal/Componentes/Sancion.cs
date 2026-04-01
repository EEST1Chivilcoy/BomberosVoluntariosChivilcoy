using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Vista.Data.Enums;
using Vista.Data.Models.Personas.Personal;

namespace Vista.Data.Models.Personas.Personal.Componentes
{
    /// <summary>
    /// Representa una sanción impuesta a un bombero.   
    /// </summary>
    public class Sancion
    {
        /// <summary>
        /// Identificador único de la sanción.
        /// </summary>
        public int SancionId { get; set; }

        /// <summary>
        /// Fecha de inicio de la sanción.
        /// </summary>
        public DateOnly FechaDesde { get; set; }

        /// <summary>
        /// Fecha de fin de la sanción.
        /// </summary>
        public DateOnly FechaHasta { get; set; }

        /// <summary>
        /// Identificador del bombero sancionado.
        /// </summary>
        public int PersonaId { get; set; }

        /// <summary>
        /// Referencia al bombero sancionado.
        /// </summary>
        [ForeignKey("PersonaId")]
        public Bombero PersonalSancionado { get; set; } = null!;

        /// <summary>
        /// Tipo de sanción.
        /// </summary>
        public TipoSancion TipoSancion { get; set; }

        /// <summary>
        /// Área de la sanción.
        /// </summary>
        public AreaSancion SacionArea { get; set; }

        /// <summary>
        /// Descripción de la sanción.
        /// </summary>
        public string? DescripcionSancion { get; set; }
    }
}
