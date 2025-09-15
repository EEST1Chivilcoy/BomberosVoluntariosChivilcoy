using Vista.Data.Enums.Socios;

namespace Vista.Data.Models.Personas.Personal
{
    public class Cobrador : Personal
    {
        /// <summary>
        /// Zonas que el cobrador tiene a su cargo
        /// </summary>
        List<Zona> ZonasAsignadas { get; set; } = new();
    }
}
