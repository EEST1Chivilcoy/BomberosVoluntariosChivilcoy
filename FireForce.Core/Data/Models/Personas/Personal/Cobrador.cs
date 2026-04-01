using System.ComponentModel.DataAnnotations;
using Vista.Data.Enums.Personal.Cobrador;
using Vista.Data.Enums.Socios;

namespace Vista.Data.Models.Personas.Personal
{
    public class Cobrador : Personal
    {
        /// <summary>
        /// Estado actual del cobrador
        /// </summary>
        [Required(ErrorMessage = "El estado del cobrador es obligatorio.")]
        public EstadoCobrador Estado { get; set; } = EstadoCobrador.Activo;

        /// <summary>
        /// Zonas que el cobrador tiene a su cargo
        /// </summary>
        [Required(ErrorMessage = "Debe asignarse al menos una zona al cobrador.")]
        public Zona ZonasAsignadas { get; set; } = Zona.Ninguna;
    }
}
