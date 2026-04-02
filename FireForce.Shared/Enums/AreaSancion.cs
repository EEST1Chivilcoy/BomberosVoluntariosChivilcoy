using System.ComponentModel.DataAnnotations;

namespace FireForce.Shared.Enums
{
    /// <summary>
    /// Representa las áreas responsables de imponer sanciones.
    /// </summary>
    public enum AreaSancion
    {
        /// <summary>
        /// Jefe
        /// </summary>
        [Display(Name = "Jefe")]
        Jefe = 1, // Valor 1 para Jefe

        /// <summary>
        /// Comisión Directiva
        /// </summary>
        [Display(Name = "Comisión Directiva")]
        ComisionDirectiva = 2 // Valor 2 para Comisión Directiva
    }
}