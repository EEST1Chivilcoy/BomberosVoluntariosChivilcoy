using System.ComponentModel.DataAnnotations;

namespace FireForce.Core.Data.Enums.Discriminadores
{
    /// <summary>
    /// Representa los tipos de socios en el sistema.
    /// </summary>
    public enum TipoSocio
    {
        /// <summary>
        /// Tipo de socio que representa a una empresa.
        /// </summary>
        [Display(Name = "Empresa")]
        Empresa = 1,

        /// <summary>
        /// Tipo de socio que representa a una persona individual.
        /// </summary>
        [Display(Name = "Persona")]
        Persona = 2,
    }
}
