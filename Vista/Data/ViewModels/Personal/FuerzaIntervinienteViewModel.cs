using System.ComponentModel.DataAnnotations;

namespace Vista.Data.ViewModels.Personal
{
    public class FuerzaIntervinienteViewModel
    {
        /// <summary>
        /// Identificador único de la fuerza interviniente en la salida.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Número de unidad de la fuerza interviniente.
        /// </summary>
        [Required(ErrorMessage = "El número de unidad es obligatorio.")]
        public string? NumeroUnidad { get; set; } = null!;

        /// <summary>
        /// Encargado de la unidad que intervino en la salida.
        /// </summary>
        [Required(ErrorMessage = "Debe especificar el apellido y nombre del encargado.")]
        public string? EncargadoApellidoyNombre { get; set; } = null!;

        /// <summary>
        /// Nombre de la fuerza a la que pertenece la unidad interviniente.
        /// </summary>
        public string? NombreFuerza { get; set; } = null!;

        /// <summary>
        /// Fuerza a la que pertenece la unidad interviniente.
        /// </summary>
        [Required(ErrorMessage = "Debe seleccionar la fuerza interviniente.")]
        public int FuerzaViewModel { get; set; }
    }
}