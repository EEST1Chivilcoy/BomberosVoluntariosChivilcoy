namespace Vista.Data.Models.Socios
{
    /// <summary>
    /// Representa un socio que es una empresa.
    /// </summary>
    public class Socio_Empresa : Socio
    {
        /// <summary>
        /// Representa la razón social de la empresa. (CUIT)
        /// </summary>
        public int RazonSocial { get; set; }
    }
}
