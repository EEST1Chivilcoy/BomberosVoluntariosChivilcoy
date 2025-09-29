using Vista.Data.Enums;
using Vista.Data.Models.Salidas.Componentes;
using Vista.Data.Models.Grupos;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;
using Vista.Data.Enums.Salidas;
using Vista.Data.Enums.Discriminadores;
using Vista.Data.Models.Personas;
using Vista.Data.Models.Personas.Personal;

namespace Vista.Data.Models.Salidas.Planillas
{

    public abstract class Salida
    {
        /// <summary>
        /// Identificador único de la salida.
        /// </summary>
        public int SalidaId { get; set; }

        /// <summary>
        /// Tipo de emergencia que originó la salida.
        /// </summary>
        public TipoDeEmergencia TipoEmergencia { get; set; }

        /// <summary>
        /// Hora de salida del cuartel.
        /// </summary>
        public DateTime HoraSalida { get; set; }

        /// <summary>
        /// Hora de llegada al lugar del incidente o al cuartel. (Preguntar a Rossi si al incidente o al cuartel)
        /// </summary>
        public DateTime HoraLlegada { get; set; }

        /// <summary>
        /// Número secuencial que representa el orden de la salida dentro del año actual.
        /// Comienza en 1 con la primera salida del año y se incrementa con cada nueva salida.
        /// Este número se reinicia al iniciar un nuevo año.
        /// </summary>
        public int NumeroParte { get; set; }

        /// <summary>
        /// Año de la salida, utilizado para identificar el periodo al que pertenece el número de parte.
        /// </summary>
        public int AnioNumeroParte { get; set; }

        /// <summary>
        /// Descripción del incidente o motivo de la salida.
        /// </summary>
        [Required, StringLength(255)]
        public string Descripcion { get; set; } = null!;

        /// <summary>
        /// Dirección del lugar donde se realizó la salida. (No es obligatorio)
        /// </summary>
        [StringLength(255)]
        public string? Direccion { get; set; }

        // Ubicación (Obligatoria) <-- Esto para hacer estadistica

        /// <summary>
        /// Latitud de la ubicación del incidente o lugar de la salida.
        /// </summary>
        [Required]
        public double Latitud { get; set; }

        /// <summary>
        /// Longitud de la ubicación del incidente o lugar de la salida.
        /// </summary>
        [Required]
        public double Longitud { get; set; }

        /// <summary>
        /// Representa a una región de Chivilcoy.
        /// </summary>
        public CuartelRegionChivilcoy? CuartelRegion { get; set; }

        /// <summary>
        /// Número de piso en caso de que la ubicación sea un departamento.
        /// </summary>
        [StringLength(255)]
        public string? PisoNumero { get; set; }

        /// <summary>
        /// Identificación o nombre del departamento.
        /// </summary>
        [StringLength(255)]
        public string? Depto { get; set; }

        /// <summary>
        /// Indica si la zona es rural o urbana.
        /// </summary>
        public TipoZona TipoZona { get; set; }

        // Datos del Solicitante

        /// <summary>
        /// Nombre de la persona que solicita la asistencia de los bomberos.
        /// </summary>
        public string? NombreSolicitante { get; set; }

        /// <summary>
        /// Apellido de la persona que solicita la asistencia de los bomberos.
        /// </summary>
        public string? ApellidoSolicitante { get; set; }

        /// <summary>
        /// Documento Nacional de Identidad (DNI) de la persona que solicita la asistencia de los bomberos.
        /// </summary>
        public string? DniSolicitante { get; set; }

        /// <summary>
        /// Teléfono de contacto de la persona que solicita la asistencia de los bomberos.
        /// </summary>
        public string? TelefonoSolicitante { get; set; }

        // Datos Receptor
        /// <summary>
        /// Nombre y apellido de la persona que recibe la solicitud de salida en el cuartel.
        /// </summary>
        public string? NombreYApellidoReceptor { get; set; }



        public List<Damnificado_Salida> Damnificados { get; set; } = new();

        public int? SeguroId { get; set; }
        public SeguroVivienda? Seguro { get; set; }


        // Moviles que asistieron al Servicio
        public List<Movil_Salida> Moviles { get; set; } = new();

        // Bomberos que asistieron al Servico

        public List<BomberoSalida> CuerpoParticipante { get; set; } = new();
        public List<FuerzaInterviniente_Salida> FuerzasIntervinientes { get; set; } = new();

        // Encargado (Obligatorio)
        public int EncargadoId { get; set; }
        [ForeignKey("EncargadoId")]
        public Bombero Encargado { get; set; } = null!;


        // Quien Lleno la Planilla (Obligatorio)
        public int QuienLlenoId { get; set; }
        [ForeignKey("QuienLlenoId")]
        public Bombero QuienLleno { get; set; } = null!;

        // Tipo de Servicio (Servicio Especial - Accidente - Rescate - Incendio)
        public TipoServicioSalida TipoServicio { get; set; }
    }
}
