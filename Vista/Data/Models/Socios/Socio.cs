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
    /// Puede ser una persona o una empresa.
    /// </summary>
    public class Socio
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
        [Required(ErrorMessage = "Debe especificar si el socio es una Empresa o una Persona.")]
        public TipoSocio? Tipo { get; set; }

        /// <summary>
        /// Representa la fecha de ingreso del socio.
        /// </summary>
        [Required(ErrorMessage = "La fecha de ingreso es obligatoria.")]
        public DateTime FechaIngreso { get; set; } = DateTime.Now;

        /// <summary>
        /// Representa el estado actual del socio. (Activo, Inactivo, Suspendido)
        /// </summary>
        [Required(ErrorMessage = "El estado del socio es obligatorio.")]
        public TipoEstadoSocio? EstadoSocio { get; set; } = TipoEstadoSocio.Activo;

        /// <summary>
        /// Representa la relación del con el historial del socio. (1 a n) (Componente)
        /// </summary>
        public List<Historial_Socio> Historial { get; set; } = new();

        /// <summary>
        /// Representa el monto actual de la cuota del socio.
        /// </summary>
        public double MontoCuota { get; set; }

        /// <summary>
        /// Representa la frecuencia de pago del socio. (Mensual, Trimestral, Semestral, Anual) (Por defecto es Mensual)
        /// </summary>
        [Required(ErrorMessage = "La frecuencia de pago es obligatoria.")]
        public FrecuenciaPago? FrecuenciaDePago { get; set; } = FrecuenciaPago.Mensual;

        // --- Datos personales ---

        /// <summary>
        /// Documento o CUIT del socio. 
        /// Documento para personas y CUIT para empresas.
        /// Debe ser único.
        /// </summary>
        [Required(ErrorMessage = "El documento o CUIT del socio es obligatorio.")]
        public string DocumentoOCUIT { get; set; } = null!;

        /// <summary>
        /// Representa el nombre de la persona o empresa.
        /// </summary>
        [Required(ErrorMessage = "El nombre del socio es obligatorio.")]
        public string? Nombre { get; set; }

        /// <summary>
        /// Representa el apellido de la persona.
        /// En caso de ser una empresa, puede estar vacío.
        /// </summary>
        public string? Apellido { get; set; } = string.Empty;

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
        [Required(ErrorMessage = "La zona del socio es obligatoria.")]
        public Zona? Zona { get; set; }

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