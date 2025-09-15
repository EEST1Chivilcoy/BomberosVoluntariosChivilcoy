using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Vista.Data.Enums;
using Vista.Data.Enums.Discriminadores;
using Vista.Data.Enums.Socios;
using Vista.Data.Models.Socios.Componentes;

namespace Vista.Data.Models.Socios
{
    /// <summary>
    /// Representa un socio en el sistema.
    /// </summary>
    public abstract class Socio
    {
        // -- Datos socio --

        /// <summary>
        /// Identificador único del socio.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)] // Evita autogeneración
        public int Id { get; set; }

        /// <summary>
        /// Representa el tipo de socio. (Empresa o Persona)
        /// </summary>
        public TipoSocio Tipo { get; set; }

        /// <summary>
        /// Representa la fecha de ingreso del socio.
        /// </summary>
        public DateTime FechaIngreso { get; set; } = DateTime.Now;

        /// <summary>
        /// Representa el estado actual del socio. (Activo, Inactivo, Suspendido)
        /// </summary>
        public TipoEstadoSocio EstadoSocio { get; set; } = TipoEstadoSocio.Activo;

        /// <summary>
        /// Representa la relación del socio con el historial del socio. (1 a n) (Componente) (Estado del socio)
        /// </summary>
        public List<HistorialEstado_Socio> HistorialEstado { get; set; } = new();

        /// <summary>
        /// Representa la relación del socio con el historial de pagos realizados por el socio. (1 a n) (Componente) (Historial de pagos)
        /// </summary>
        public List<HistorialPago_Socio> HistorialPagos { get; set; } = new();

        /// <summary>
        /// Representa la relación del socio con el historial de cuotas del socio. (1 a n) (Componente) (Historial de cuotas) (Valor de las cuotas a lo largo del tiempo)
        /// </summary>
        public List<HistorialCuota_Socio> HistorialCuotas { get; set; } = new();

        /// <summary>
        /// Representa el monto actual de la cuota del socio.
        /// </summary>
        public double MontoCuota { get; set; }

        /// <summary>
        /// Representa la frecuencia de pago del socio. (Mensual, Trimestral, Semestral, Anual) (Por defecto es Mensual)
        /// </summary>
        public FrecuenciaPago FrecuenciaDePago { get; set; } = FrecuenciaPago.Mensual;

        // --- Datos personales ---

        /// <summary>
        /// Representa el nombre de la persona o empresa.
        /// </summary>
        public string Nombre { get; set; } = string.Empty;

        /// <summary>
        /// Representa la localidad del socio.
        /// </summary>
        public string Localidad { get; set; } = string.Empty;

        /// <summary>
        /// Representa la dirección del socio.
        /// </summary>
        public string Direccion { get; set; } = string.Empty;

        /// <summary>
        /// Representa la Latitud del socio. (Dirección GPS)
        /// </summary>
        public double? Latitud { get; set; }

        /// <summary>
        /// Representa la Longitud del socio. (Dirección GPS)
        /// </summary>
        public double? Longitud { get; set; }

        /// <summary>
        /// Representa la zona del socio.
        /// </summary>
        public TipoZona Zona { get; set; }

        /// <summary>
        /// Representa la ocupación del socio.
        /// </summary>
        public string? Ocupacion { get; set; }

        /// <summary>
        /// Representa el número de teléfono del socio.
        /// </summary>
        public string Telefono { get; set; } = string.Empty;
    }
}