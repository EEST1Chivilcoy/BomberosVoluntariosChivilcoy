using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Vista.Data.Enums.Personal.Cobrador
{
    /// <summary>
    /// Representa los posibles estados de un cobrador.
    /// </summary>
    public enum EstadoCobrador
    {
        /// <summary>
        /// Estado activo del cobrador.
        /// </summary>
        [Display(Name = "Activo")]
        Activo = 1,

        /// <summary>
        /// Estado inactivo del cobrador.
        /// </summary>
        [Display(Name = "Inactivo")]
        Inactivo = 2,

        /// <summary>
        /// Estado suspendido del cobrador.
        /// </summary>
        [Display(Name = "Suspendido")]
        Suspendido = 3,

        /// <summary>
        /// Estado de baja por edad del cobrador.
        /// </summary>
        [Display(Name = "Baja por Edad")]
        BajaPorEdad = 6,

        /// <summary>
        /// Estado de cobrador fallecido.
        /// </summary>
        [Display(Name = "Fallecido")]
        Fallecido = 7
    }
}
