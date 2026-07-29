using System.ComponentModel.DataAnnotations;
using FireForce.Shared.Enums.Discriminadores;
using FireForce.Shared.Enums.Firmas;

namespace FireForce.Client.Data.ViewModels.Incidentes
{
    /// <summary>
    /// Datos del formulario de alta de un incidente.
    /// </summary>
    /// <remarks>
    /// Los DataAnnotations son la validación de PRIMERA capa: la que corre en el formulario y le
    /// avisa al usuario mientras carga. Las reglas de negocio (quién puede reportar qué, de dónde
    /// sale el encargado) viven en <c>IncidenteService</c>, que es la única fuente de verdad.
    /// No implementa <c>IEditableViewModel</c> porque ese patrón (editar sobre una copia con
    /// Clonar/ActualizarDesde) aplica a modales de edición, y los incidentes no se editan: se
    /// crean y después se resuelven.
    /// </remarks>
    public class IncidenteFormViewModel
    {
        public TipoDeIncidente Tipo { get; set; } = TipoDeIncidente.Movil;

        public CategoriaIncidente Categoria { get; set; } = CategoriaIncidente.Novedad;

        [Required(ErrorMessage = "La descripción es obligatoria.")]
        [StringLength(1000, MinimumLength = 5,
            ErrorMessage = "La descripción debe tener entre 5 y 1000 caracteres.")]
        public string Descripcion { get; set; } = string.Empty;

        [Range(1, int.MaxValue, ErrorMessage = "Seleccioná quién reporta el incidente.")]
        public int SolicitanteId { get; set; }

        /// <summary>Móvil afectado. Obligatorio solo cuando el tipo es Móvil.</summary>
        public int? VehiculoId { get; set; }

        /// <summary>Dependencia afectada. Obligatoria solo cuando el tipo es Dependencia.</summary>
        public int? DependenciaId { get; set; }

        /// <summary>
        /// Se completa desde la UI al elegir el solicitante. Permite avisar en el formulario que
        /// un no bombero solo puede reportar novedades, sin esperar al error del servidor.
        /// </summary>
        public bool SolicitanteEsBombero { get; set; }

        /// <summary>
        /// Valida lo que los DataAnnotations no pueden expresar por depender de otro campo.
        /// </summary>
        public IEnumerable<string> ObtenerErroresDeCoherencia()
        {
            if (Tipo == TipoDeIncidente.Movil && VehiculoId is null or 0)
            {
                yield return "Seleccioná el móvil afectado.";
            }

            if (Tipo == TipoDeIncidente.Dependencia && DependenciaId is null or 0)
            {
                yield return "Seleccioná la dependencia afectada.";
            }

            if (SolicitanteId > 0 && !SolicitanteEsBombero && Categoria != CategoriaIncidente.Novedad)
            {
                yield return "Quien reporta no es bombero, así que el incidente solo puede ser una novedad.";
            }
        }
    }
}
