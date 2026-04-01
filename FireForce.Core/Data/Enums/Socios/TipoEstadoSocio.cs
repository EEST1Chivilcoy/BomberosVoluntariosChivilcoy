using System.ComponentModel.DataAnnotations;

namespace Vista.Data.Enums.Socios
{
    /// <summary>
    /// Representa los diferentes estados en los que puede encontrarse un socio.
    /// </summary>
    public enum TipoEstadoSocio
    {
        /// <summary>
        /// Representa un socio activo.
        /// </summary>
        [Display(Name = "Activo")]
        Activo = 1,

        /// <summary>
        /// Representa un socio inactivo.
        /// </summary>
        [Display(Name = "Inactivo")]
        Inactivo = 2,

        /// <summary>
        /// Representa un socio suspendido.
        /// </summary>
        [Display(Name = "Suspendido")]
        Suspendido = 3
    }
}
