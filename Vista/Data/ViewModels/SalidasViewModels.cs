using Vista.Data.Enums;
using Vista.Data.Models.Salidas.Componentes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Vista.Data.Enums.Salidas;
using Vista.Data.Enums.Discriminadores;
using Vista.Data.Models.Personas;
using Vista.DTOs.Nominatim;
using Vista.Data.Models.Grupos.FuerzasIntervinientes;
using Vista.Data.ViewModels.Personal;

namespace Vista.Data.ViewModels
{
    /// <summary>
    /// ViewModel base para todas las salidas.
    /// </summary>
    public abstract class SalidasViewModels
    {
        // / --- Sección de Identificación General ---

        /// <summary>
        /// Identificador único de la salida.
        /// </summary>
        public int SalidaId { get; set; }

        /// <summary>
        /// Tipo de emergencia que originó la salida.
        /// </summary>
        [Required]
        public TipoDeEmergencia TipoEmergencia { get; set; }

        // --- Sección de Identificación de la Salida ---

        /// <summary>
        /// Numero de salida único. (Del año actual).
        /// </summary>
        [Required]
        public int NumeroParte { get; set; }

        /// <summary>
        /// Año del número de parte.
        /// </summary>
        [Required]
        public int AnioNumeroParte { get; set; }

        // --- Sección de Fechas y Horas ---

        /// <summary>
        /// Fecha de la salida. (Obligatoria) (Debería ser la fecha de la salida del cuartel).
        /// </summary>
        [Required]
        public DateTime FechaSalida { get; set; }

        /// <summary>
        /// Fecha de llegada. (Obligatoria) (Debería ser la fecha de llegada al cuartel).
        /// </summary>
        [Required]
        public DateTime FechaLlegada
        {
            get
            {
                // Calcula la fecha de llegada basándose en la fecha de salida y las horas de llegada y salida.
                return TimeLlegada < TimeSalida ? FechaSalida.AddDays(1) : FechaSalida;
            }
        }

        /// <summary>
        /// Hora de salida del cuartel.
        /// </summary>
        [Required]
        public TimeOnly TimeSalida { get; set; }

        /// <summary>
        /// Hora de llegada al cuartel.
        /// </summary>
        [Required]
        public TimeOnly TimeLlegada { get; set; }

        /// <summary>
        /// Representa la hora de salida combinando la fecha de salida y la hora de salida.
        /// </summary>
        [Required]
        public DateTime HoraSalida
        {
            get
            {
                // Combina FechaSalida con TimeSalida
                return FechaSalida.Date.Add(TimeSalida.ToTimeSpan());
            }
        }

        /// <summary>
        /// Representa la hora de llegada combinando la fecha de llegada y la hora de llegada.
        /// </summary>
        [Required]
        public DateTime HoraLLegada
        {
            get
            {
                // Combina FechaLlegada con TimeLlegada
                return FechaLlegada.Date.Add(TimeLlegada.ToTimeSpan());
            }
        }

        /// <summary>
        /// Guardia que está a cargo de la salida.
        /// Esta propiedad representa la guardia seleccionada para la salida.
        /// </summary>
        public Guardia? GuardiaSelecionada { get; set; }

        /// <summary>
        /// Descripción del incidente o motivo de la salida.
        /// </summary>
        [Required]
        public string? Descripcion { get; set; }

        // --- Ubicación ---

        /// <summary>
        /// Dirección donde ocurrió el incidente o lugar de la salida.
        /// </summary>
        [Required]
        public string? Direccion { get; set; }

        /// <summary>
        /// Latitud de la ubicación del incidente o lugar de la salida.
        /// </summary>
        [Required]
        public double? Latitud { get; set; }

        /// <summary>
        /// Longitud de la ubicación del incidente o lugar de la salida.
        /// </summary>
        [Required]
        public double? Longitud { get; set; }

        // Datos por si es en un edificio. (Opcional) (Apartamento, Piso, etc.)

        /// <summary>
        /// Piso donde ocurrió el incidente.
        /// </summary>
        [StringLength(50)]
        public string? PisoNumero { get; set; }

        /// <summary>
        /// Departamento o unidad dentro del edificio donde ocurrió el incidente.
        /// </summary>
        [StringLength(50)]
        public string? Depto { get; set; }

        /// <summary>
        /// Tipo de zona donde ocurrió el incidente.
        /// Puede ser Urbana, Rural.
        /// </summary>
        [Required]
        public TipoZona? TipoZona { get; set; }

        /// <summary>
        /// Cuartel o región donde ocurrió el incidente.
        /// </summary>
        public CuartelRegionChivilcoy? CuartelRegion { get; set; }

        // --- Sección de Datos del Solicitante ---

        /// <summary>
        /// Nombre del solicitante de la salida. (Del que llamó y aviso del incidente).
        /// Debería ser obligatorio.
        /// </summary>
        [StringLength(100)]
        public string? NombreSolicitante { get; set; }

        /// <summary>
        /// Apellido del solicitante de la salida. (Del que llamó y aviso del incidente).
        /// Debería ser obligatorio.
        /// </summary>
        [StringLength(100)]
        public string? ApellidoSolicitante { get; set; }

        /// <summary>
        /// Documento Nacional de Identidad del solicitante. (DNI) (Del que llamó y aviso del incidente).
        /// No es obligatorio, pero se recomienda.
        /// </summary>
        public string? DniSolicitante { get; set; }

        /// <summary>
        /// Telefono del solicitante de la salida. (Del que llamó y aviso del incidente).
        /// Debería ser obligatorio.
        /// </summary>
        public string? TelefonoSolicitante { get; set; }

        // --- Datos del Recepcionista de la salida ---

        /// <summary>
        /// Tipo de receptoria de la salida.
        /// </summary>

        public TipoReceptoria TipoReceptoria { get; set; } = TipoReceptoria.Casero;

        /// <summary>
        /// Nombre del receptor de la salida.
        /// </summary>
        public string? NombreReceptor { get; set; }

        /// <summary>
        /// Apellido del receptor de la salida.
        /// </summary>
        public string? ApellidoReceptor { get; set; }

        /// <summary>
        /// ID del bombero que recepcionó la salida. (Puede ser null si no es un bombero).
        /// </summary>
        public int? BomberoReceptor { get; set; }

        // --- Sección de Datos Adicionales ---

        public List<FuerzaIntervinienteViewModel> FuerzasIntervinientes { get; set; } = new();

        public List<Damnificado_Salida> Damnificados { get; set; } = new();

        public List<VehiculoAfectado> VehiculosAfectados { get; set; } = new();

        // --- Sección del Seguro ---

        public string? CompaniaAseguradora { get; set; }
        public string? NumeroPoliza { get; set; }
        public DateTime? FechaVencimineto { get; set; }

        // --- Sección de Recursos Moviles y Humanos ---

        public List<Movil_Salida> Moviles { get; set; } = new();
        public List<BomberoSalida> CuerpoParticipante { get; set; } = new();

        public List<BomberoViweModel> BomberosParticipantes { get; set; } = new();

        // --- Sección de Datos de la Planilla ---

        [Required]
        public int BomberoEncargadoId { get; set; }

        [Required]
        public int BomberoPlanillaId { get; set; }

        [Required]
        public TipoServicioSalida? TipoServicio { get; set; } = TipoServicioSalida.AsistenciaAccidental;
    }
}
