using Vista.Data.Enums;
using Vista.Data.Models.Salidas.Componentes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Vista.Data.Enums.Salidas;
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
        /// <summary>
        /// Identificador único de la salida.
        /// </summary>
        public int SalidaId { get; set; }

        // Sección de Datos pasados por Parametros.

        /// <summary>
        /// Numero de salida único. (Del año actual).
        /// </summary>
        public int NumeroParte { get; set; }

        /// <summary>
        /// Año del número de parte.
        /// </summary>
        public int AnioNumeroParte { get; set; }

        // Sección de Datos Generales de la Salida.

        /// <summary>
        /// Fecha de la salida. (Obligatoria) (Debería ser la fecha de la salida del cuartel).
        /// </summary>
        public DateTime FechaSalida { get; set; }

        /// <summary>
        /// Fecha de llegada. (Obligatoria) (Debería ser la fecha de llegada al cuartel).
        /// </summary>
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
        public TimeOnly TimeSalida { get; set; }

        /// <summary>
        /// Hora de llegada al cuartel.
        /// </summary>
        public TimeOnly TimeLlegada { get; set; }

        /// <summary>
        /// Representa la hora de salida combinando la fecha de salida y la hora de salida.
        /// </summary>
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
        public DateTime HoraLLegada
        {
            get
            {
                // Combina FechaLlegada con TimeLlegada
                return FechaLlegada.Date.Add(TimeLlegada.ToTimeSpan());
            }
        }

        // Pendiente de revisar por cambios en la funcionalidad.
        public Direccion Direccion { get; set; }

        /// <summary>
        /// Guardia que está a cargo de la salida.
        /// Esta propiedad representa la guardia seleccionada para la salida. (Si se convocó a una guardia específica).
        /// </summary>
        public Guardia GuardiaSelecionada { get; set; }

        /// <summary>
        /// Fuerzas intervinientes que participaron en la salida.
        /// </summary>
        public IEnumerable<SimpleFuerzaViewModel> FuerzasIntervinientesParticipantes { get; set; } = new List<SimpleFuerzaViewModel>();

        /// <summary>
        /// Descripción del incidente o motivo de la salida.
        /// </summary>
        [Required, StringLength(255)]
        public string Descripcion { get; set; } = null!;

        /// <summary>
        /// Nombre de la calle o ruta donde ocurrió el incidente.
        /// </summary>
        [StringLength(255)]
        public string? CalleORuta { get; set; }

        /// <summary>
        /// Altura o kilómetro donde ocurrió el incidente.
        /// </summary>
        public int? AlturaOKm { get; set; }

        // Datos por si es en un edificio. (Opcional) (Apartamento, Piso, etc.)

        /// <summary>
        /// Piso donde ocurrió el incidente.
        /// </summary>
        [StringLength(255)]
        public string? PisoNumero { get; set; }

        /// <summary>
        /// Departamento o unidad dentro del edificio donde ocurrió el incidente.
        /// </summary>
        [StringLength(255)]
        public string? Depto { get; set; }

        /// <summary>
        /// Tipo de zona donde ocurrió el incidente.
        /// Puede ser Urbana, Rural.
        /// </summary>
        public TipoZona TipoZona { get; set; }

        /// <summary>
        /// Cuartel o región donde ocurrió el incidente.
        /// </summary>
        public CuartelRegionChivilcoy CuartelRegion { get; set; }

        // Ubicación <-- Esto para hacer estadística (No es obligatorio por que faltan calles) (La API Nominatim devuelve la ubicación)

        /// <summary>
        /// Latitud de la ubicación del incidente o lugar de la salida.
        /// </summary>
        public double Latitud { get; set; }

        /// <summary>
        /// Longitud de la ubicación del incidente o lugar de la salida.
        /// </summary>
        public double Longitud { get; set; }

        // Sección de Datos del Solicitante y Receptor. (Datos telefónicos)

        // Datos del solicitante de la salida.

        /// <summary>
        /// Nombre del solicitante de la salida. (Del que llamó y aviso del incidente).
        /// Debería ser obligatorio.
        /// </summary>
        public string NombreSolicitante { get; set; } = null!;

        /// <summary>
        /// Apellido del solicitante de la salida. (Del que llamó y aviso del incidente).
        /// Debería ser obligatorio.
        /// </summary>
        public string ApellidoSolicitante { get; set; } = null!;

        /// <summary>
        /// Documento Nacional de Identidad del solicitante. (DNI) (Del que llamó y aviso del incidente).
        /// No es obligatorio, pero se recomienda.
        /// </summary>
        public string? DniSolicitante { get; set; }

        /// <summary>
        /// Telefono del solicitante de la salida. (Del que llamó y aviso del incidente).
        /// Debería ser obligatorio.
        /// </summary>
        public string TelefonoSolicitante { get; set; } = null!;

        // Datos del receptor de la salida.

        /// <summary>
        /// Nombre y apellido del receptor de la salida.
        /// </summary>
        public string? NombreYApellidoReceptor
        {
            get
            {
                if (string.IsNullOrEmpty(ApellidoReceptor) && string.IsNullOrEmpty(NombreReceptor))
                    return null;

                // Combina el apellido y el nombre con una coma y un espacio
                return $"{ApellidoReceptor}, {NombreReceptor}";
            }
        }

        /// <summary>
        /// Nombre del receptor de la salida.
        /// </summary>
        public string? NombreReceptor { get; set; }

        /// <summary>
        /// Apellido del receptor de la salida.
        /// </summary>
        public string? ApellidoReceptor { get; set; }

        // Sección de Datos Adicionales.

        public List<FuerzaIntervinienteViewModel> FuerzasIntervinientes { get; set; } = new();

        public List<Damnificado_Salida> Damnificados { get; set; } = new();

        //datos del seguro 
        public string? CompaniaAseguradora { get; set; }
        public string? NumeroPoliza { get; set; }
        public DateTime? FechaVencimineto { get; set; }

        public List<Movil_Salida> Moviles { get; set; } = new();
        public List<BomberoSalida> CuerpoParticipante { get; set; } = new();

        //Bombero encargado
        public string NombreEncargado { get; set; }
        public string ApellidoEncargado { get; set; }
        public int LegajoEncargado { get; set; }
        //bombero que lleno la planilla
        public string NombreLLenoPlanilla { get; set; }
        public string ApllidoLLenoPlanilla { get; set; }
        public string NombreYApellidoLlenoPlanilla
        {
            get { return NombreLLenoPlanilla + " " + ApllidoLLenoPlanilla; }
        }
        public int LegajoLLenoPlanilla { get; set; }
        public TipoServicioSalida TipoServicio { get; set; }

        public TipoServicioRepresentaciones TipoRepresentacion { get; set; }
        public TipoReceptoria TipoReceptoria { get; set; } = TipoReceptoria.Casero;
    }
}
