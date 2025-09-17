using Vista.Data.Enums;
using Vista.Data.Models.Salidas.Componentes;
using Vista.Data.Models.Salidas.Planillas;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Vista.Data.Models.Grupos.Dependencias;
using Vista.Data.Models.Grupos.Brigadas;
using Vista.Data.Models.Imagenes;
using Vista.Data.Models.Grupos.Dependencias.Comunicaciones;
using Vista.Data.Models.Personas.Personal.Componentes;
using Vista.Data.Models.Vehiculos.Flota;
using Vista.Data.Models.Vehiculos.Flota.Componentes;
using Vista.Data.Models.Otros;
using Vista.Data.Models.Objetos.Componentes;

namespace Vista.Data.Models.Personas.Personal
{
    /// <summary>
    /// Representa a un bombero, heredando las características del personal.
    /// Incluye información sobre su estado, dotación, grado, características físicas y relaciones con otras entidades.
    /// </summary>
    [Index(nameof(NumeroLegajo))]
    [Index(nameof(EquipoId), IsUnique = true)]
    public class Bombero : Personal
    {
        /// <summary>
        /// Número de legajo único del bombero.
        /// </summary>
        [Required]
        public int NumeroLegajo { get; set; }

        /// <summary>
        /// Estado actual del bombero (activo, baja, etc.).
        /// </summary>
        public EstadoBombero Estado { get; set; }

        /// <summary>
        /// Observaciones adicionales sobre la persona.
        /// </summary>
        public string? Observaciones { get; set; }

        /// <summary>
        /// Profesión de la persona. Longitud máxima de 255 caracteres.
        /// </summary>
        [StringLength(255)]
        public string? Profesion { get; set; }

        /// <summary>
        /// Nivel de estudios alcanzado por la persona. Longitud máxima de 255 caracteres.
        /// </summary>
        [StringLength(255)]
        public string? NivelEstudios { get; set; }

        /// <summary>
        /// Número de IOMA (cobertura de salud). Longitud máxima de 255 caracteres.
        /// </summary>
        [StringLength(255)]
        public string? NumeroIoma { get; set; }

        /// <summary>
        /// Dotación a la que pertenece el bombero.
        /// </summary>
        public TipoDotaciones Dotacion { get; set; }

        /// <summary>
        /// Grado jerárquico del bombero dentro del escalafón.
        /// </summary>
        public EscalafonJerarquico Grado { get; set; }

        /// <summary>
        /// Altura del bombero en centímetros.
        /// </summary>
        public int? Altura { get; set; }

        /// <summary>
        /// Peso del bombero en kilogramos.
        /// </summary>
        public int? Peso { get; set; }

        /// <summary>
        /// Indica si el bombero está habilitado como chofer.
        /// </summary>
        public bool Chofer { get; set; }

        /// <summary>
        /// Fecha de vencimiento del registro de conducir del bombero.
        /// </summary>
        public DateTime? VencimientoRegistro { get; set; }

        /// <summary>
        /// Firmas registradas del bombero.
        /// </summary>
        public List<Firma>? Firmas { get; set; }

        /// <summary>
        /// Relación muchos a muchos entre bomberos y brigadas.
        /// </summary>
        public List<Bombero_Brigada> Brigadas { get; set; } = new List<Bombero_Brigada>();

        /// <summary>
        /// Lista de vehículos a cargo del bombero.
        /// </summary>
        public List<VehiculoSalida>? VehiculosEncargado { get; set; }

        /// <summary>
        /// Relación muchos a muchos entre bomberos y dependencias.
        /// </summary>
        public List<Bombero_Dependencia> Dependencias { get; set; } = new();

        /// <summary>
        /// Incidentes en los que ha participado el bombero.
        /// </summary>
        public List<Incidente> Incidentes { get; set; } = new();

        /// <summary>
        /// Salidas de emergencia en las que ha participado el bombero.
        /// </summary>
        public List<BomberoSalida> Salidas { get; set; } = new();

        /// <summary>
        /// Identificador del equipo de comunicación asignado al bombero.
        /// </summary>
        public int? EquipoId { get; set; }

        /// <summary>
        /// Dispositivo de comunicación (Handie) asignado al bombero.
        /// </summary>
        public Comunicacion? Handie { get; set; }

        /// <summary>
        /// Historial de ascensos del bombero.
        /// </summary>
        public List<AscensoBombero> Ascensos { get; set; } = new();

        /// <summary>
        /// Materiales que el bombero ha gestionado como destino.
        /// </summary>
        public List<MovimientoMaterial> DestinoMaterial { get; set; } = new();

        /// <summary>
        /// Sanciones recibidas por el bombero.
        /// </summary>
        [InverseProperty("PersonalSancionado")]
        public List<Sancion> SancionesRecibidas { get; set; } = new();

        /// <summary>
        /// Registros de limpieza en los que ha participado el bombero.
        /// </summary>
        public List<Limpieza> Limpieza { get; set; } = new();

        /// <summary>
        /// Novedades registradas por el bombero.
        /// </summary>
        public List<NovedadBase>? Novedades { get; set; } = new();

        /// <summary>
        /// Lista de licencias asociadas a el bombero. Inicializada como una lista vacía por defecto.
        /// </summary>
        public List<Licencia> Licencias { get; set; } = new();

    }
}
