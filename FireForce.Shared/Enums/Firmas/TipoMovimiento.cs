using System.ComponentModel.DataAnnotations;

namespace FireForce.Shared.Enums.Firmas
{
    /// <summary>
    /// Tipo de entrada en la bitácora de un incidente.
    /// </summary>
    public enum TipoMovimiento
    {
        [Display(Name = "Creación")]
        Creacion,

        [Display(Name = "Cambio de estado")]
        CambioDeEstado,

        // Derivar un incidente a otro departamento es, justamente, crearle una tarea.
        [Display(Name = "Derivación")]
        CreacionDeTarea
    }
}
