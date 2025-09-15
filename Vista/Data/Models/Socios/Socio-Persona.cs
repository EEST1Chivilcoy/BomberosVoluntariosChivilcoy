namespace Vista.Data.Models.Socios
{
    /// <summary>
    /// Representa un socio que es una persona individual.
    /// </summary>
    public class Socio_Persona : Socio
    {
        /// <summary>
        /// Representa el documento de identidad de la persona.
        /// </summary>
        public int Documento { get; set; }

        /// <summary>
        /// Representa el apellido de la persona.
        /// </summary>
        public string Apellido { get; set; } = string.Empty;
    }
}
