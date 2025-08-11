using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Vista.Data.Enums;
using Vista.Data.Models.Personas.Personal;

namespace Vista.Data.Models.Personas.Personal.Componentes
{
    /// <summary>
    /// Representa una licencia asociada a alguien del personal.
    /// </summary>
    public class Licencia
    {
        /// <summary>
        /// Identificador único de la licencia.
        /// </summary>
        public int LicenciaId { get; set; }

        /// <summary>
        /// Tipo de licencia asociada. Este campo es obligatorio.
        /// </summary>
        [Required]
        public TipoLicencia TipoLicencia { get; set; }

        /// <summary>
        /// Descripción opcional de la licencia.
        /// </summary>
        public string? Descripcion { get; set; }

        /// <summary>
        /// Fecha de inicio de la licencia.
        /// </summary>
        public DateTime Desde { get; set; }

        /// <summary>
        /// Fecha de finalización de la licencia.
        /// </summary>
        public DateTime Hasta { get; set; }

        /// <summary>
        /// Estado actual de la licencia (por ejemplo, activa, vencida).
        /// </summary>
        public TipoEstadoLicencia EstadoLicencia { get; set; }

        /// <summary>
        /// Identificador único de la personal a la que se asigna la licencia.
        /// </summary>
        public int PersonalId { get; set; }
        [ForeignKey("PersonalId")]

        /// <summary>
        /// Información del personal al que está asignada la licencia. Es una relación foránea.
        /// Esta propiedad es obligatoria.
        /// </summary>
        public Bombero BomberoAfectado { get; set; } = null!;
    }
}
