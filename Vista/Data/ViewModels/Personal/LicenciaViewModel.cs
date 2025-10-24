using System.ComponentModel.DataAnnotations;
using Vista.Data.Enums;

namespace Vista.Data.ViewModels.Personal
{
    /// <summary>
    /// ViewModel para la gestión de licencias (alta, baja, modificación y listado).
    /// </summary>
    public class LicenciaViewModel
    {
        /// <summary>
        /// Id de la licencia.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Tipo de licencia.
        /// </summary>
        [Required(ErrorMessage = "El tipo de licencia es obligatorio.")]
        public TipoLicencia? TipoLicencia { get; set; }

        /// <summary>
        /// Numero de legajo del bombero afectado por la licencia.
        /// </summary>
        public int NumeroLegajo { get; set; }

        /// <summary>
        /// Id del bombero afectado por la licencia.
        /// </summary>
        public int PersonaId { get; set; }

        /// <summary>
        /// Bombero afectado por la licencia.
        /// </summary>
        public BomberoViweModel? PersonalAfectado { get; set; }

        /// <summary>
        /// Apellido y nombre del bombero afectado por la licencia.
        /// </summary>
        public string? ApellidoYNombre { get; set; }

        /// <summary>
        /// Descripción de la licencia.
        /// </summary>
        public string? Descripcion { get; set; }

        /// <summary>
        /// Fecha de inicio de la licencia.
        /// Esta fecha debe ser mayor o igual a la fecha actual.
        /// Es obligatoria.
        /// </summary>
        [Required(ErrorMessage = "La fecha de inicio es obligatoria.")]
        public DateTime Desde { get; set; }

        /// <summary>
        /// Fecha de finalización de la licencia.
        /// Esta fecha debe ser mayor o igual a la fecha de inicio.
        /// Es obligatoria.
        /// </summary>
        [Required(ErrorMessage = "La fecha de fin es obligatoria.")]
        public DateTime Hasta { get; set; }

        /// <summary>
        /// Estado de la licencia. (Aprobada, Rechazada, Pendiente)
        /// La licencia se encuentra en estado pendiente hasta que se aprueba o rechaza.
        /// </summary>
        public TipoEstadoLicencia EstadoLicencia { get; set; }

        /// <summary>
        /// Razon de rechazo de la licencia, si aplica.
        /// </summary>
        public string? RazonRechazo { get; set; }

        /// <summary>
        /// Duración de la licencia en días (inclusive).
        /// </summary>
        public int DuracionEnDias => (Hasta - Desde).Days + 1;

        /// <summary>
        /// Indica si las fechas cumplen con la lógica de validación.
        /// </summary>
        public bool FechasValidas => Desde >= DateTime.Today && Hasta >= Desde;
    }
}
