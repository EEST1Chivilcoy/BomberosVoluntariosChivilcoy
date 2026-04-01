using Vista.Data.Enums;
using Vista.Data.Models.Salidas.Componentes;
using Vista.Data.Models.Salidas.Planillas;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;
using Vista.Data.Models.Personas.Personal;
using Vista.Data.Models.Vehiculos.Flota;

namespace Vista.Data.Models.Grupos.Dependencias.Comunicaciones
{
    [Index(nameof(NroEquipo), IsUnique = true)]
    public class Comunicacion
    {
        /// <summary>
        /// Identificador único del equipo de comunicación.
        /// </summary>
        public int ComunicacionId { get; set; }

        /// <summary>
        /// Número único del equipo de comunicación (por ejemplo, número de serie o código identificador).
        /// Este campo debe ser único en el sistema.
        /// </summary>
        [Required]
        public string NroEquipo { get; set; } = null!;

        /// <summary>
        /// Marca del equipo de comunicación (por ejemplo, Motorola, Kenwood, etc.).
        /// Este campo es opcional.
        /// </summary>
        public string? Marca { get; set; }

        /// <summary>
        /// Modelo del equipo de comunicación (por ejemplo, "XT 2000").
        /// Este campo es opcional.
        /// </summary>
        public string? Modelo { get; set; }

        /// <summary>
        /// Número de serie único del equipo.
        /// Este campo es opcional.
        /// </summary>
        public string? NroSerie { get; set; }

        /// <summary>
        /// Estado actual del equipo de comunicación, representado por un tipo enumerado (por ejemplo, "Activo", "En reparación").
        /// </summary>
        public TipoEstadoHandie Estado { get; set; }

        /// <summary>
        /// Bombero asignado al equipo de comunicación. 
        /// Esta relación es opcional y puede ser nula si el equipo no está asignado a un bombero específico.
        /// </summary>
        public Bombero? Bombero { get; set; }

        /// <summary>
        /// Vehículo o unidad móvil asociada al equipo de comunicación.
        /// Esta relación es opcional y puede ser nula si no hay un vehículo asociado.
        /// </summary>
        public Movil? Movil { get; set; }
    }
}
