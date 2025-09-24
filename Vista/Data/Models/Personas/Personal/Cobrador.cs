using Vista.Data.Enums.Personal.Cobrador;
using Vista.Data.Enums.Socios;

namespace Vista.Data.Models.Personas.Personal
{
    public class Cobrador : Personal
    {
        /// <summary>
        /// Estado actual del cobrador
        /// </summary>
        public EstadoCobrador Estado { get; set; } = EstadoCobrador.Activo;

        /// <summary>
        /// Zonas que el cobrador tiene a su cargo
        /// </summary>
        public Zona ZonasAsignadas { get; set; } = Zona.Ninguna;
    }
}
