using System.ComponentModel.DataAnnotations;

namespace Vista.Data.Enums.Personal.ComisionDirectiva
{
    public enum GradoComisionDirectiva
    {
        /// <summary>
        /// Presidente de la Comisión Directiva.
        /// </summary>
        [Display(Name = "Presidente")]
        Presidente = 1,

        /// <summary>
        /// Vicepresidente de la Comisión Directiva.
        /// </summary>
        [Display(Name = "Vice-Presidente")]
        VicePresidente = 2,

        /// <summary>
        /// Secretario de la Comisión Directiva.
        /// </summary>
        [Display(Name = "Secretario")]
        Secretario = 3,

        /// <summary>
        /// Pro-Secretario de la Comisión Directiva.
        /// </summary>
        [Display(Name = "Pro-Secretario")]
        ProSecretario = 4,

        /// <summary>
        /// Tesorero de la Comisión Directiva.
        /// </summary>
        [Display(Name = "Tesorero")]
        Tesorero = 5,

        /// <summary>
        /// Pro-Tesorero de la Comisión Directiva.
        /// </summary>
        [Display(Name = "Pro-Tesorero")]
        ProTesorero = 6,

        /// <summary>
        /// Secretario de Actas de la Comisión Directiva.
        /// </summary>
        [Display(Name = "Secretario de Actas")]
        SecretarioDeActas = 7,

        /// <summary>
        /// Secretario de Prensa de la Comisión Directiva.
        /// </summary>
        [Display(Name = "Intendente de Sede")]
        IntendenteDeSede = 8,

        /// <summary>
        /// Vocal Titular de la Comisión Directiva.
        /// </summary>
        [Display(Name = "Vocal Titular")]
        VocalTitular = 9,

        /// <summary>
        /// Vocal Suplente de la Comisión Directiva.
        /// </summary>
        [Display(Name = "Vocal Suplente")]
        VocalSuplente = 10,

        /// <summary>
        /// Revisor de Cuentas Titular de la Comisión Directiva.
        /// </summary>
        [Display(Name = "Revisor de Cuentas Titular")]
        RevisorCuentasTitular = 11,

        /// <summary>
        /// Revisor de Cuentas Suplente de la Comisión Directiva.
        /// </summary>
        [Display(Name = "Revisor de Cuentas Suplente")]
        RevisorCuentasSuplente = 12
    }
}
