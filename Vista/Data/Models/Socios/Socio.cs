using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Vista.Data.Enums;
using Vista.Data.Enums.Discriminadores;
using Vista.Data.Enums.Socios;
using Vista.Data.Models.Socios.Componentes;
using Vista.Data.Models.Socios.Componentes.Pagos;

namespace Vista.Data.Models.Socios
{
    /// <summary>
    /// Representa un socio en el sistema.
    /// Puede ser una persona o una empresa.
    /// </summary>
    [Index(nameof(NroSocio), IsUnique = true)]
    public class Socio
    {
        // -- Datos socio --

        /// <summary>
        /// Identificador único del socio.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Numero de Socio (Número correlativo asignado automáticamente). (Único)
        /// </summary>
        [Required(ErrorMessage = "El número de socio es obligatorio.")]
        public int NroSocio { get; set; }

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
        /// Representa la fecha de ingreso del socio al nuevo sistema.
        /// Ya habia socios antes de implementar el sistema.
        /// Esta fecha es para calcular correctamente las cuotas y pagos.
        /// </summary>
        [Required(ErrorMessage = "La fecha de ingreso al nuevo sistema es obligatoria.")]
        public DateTime FechaIngresoSistemaNuevo { get; set; } = DateTime.Now;

        /// <summary>
        /// Representa el estado actual del socio. (Activo, Inactivo, Suspendido)
        /// </summary>
        [Required(ErrorMessage = "El estado del socio es obligatorio.")]
        public TipoEstadoSocio? EstadoSocio { get; set; } = TipoEstadoSocio.Activo;

        /// <summary>
        /// Representa la relación del con el historial del socio. (1 a n) (Componente)
        /// </summary>
        public List<MovimientoSocio> Historial { get; set; } = new();

        /// <summary>
        /// Representa el monto actual de la cuota del socio.
        /// </summary>
        public double MontoCuota { get; set; }

        /// <summary>
        /// Representa la frecuencia de pago del socio. (Mensual, Trimestral, Semestral, Anual)
        /// </summary>
        [Required(ErrorMessage = "La frecuencia de pago es obligatoria.")]
        public FrecuenciaPago? FrecuenciaDePago { get; set; }

        /// <summary>
        /// Representa la forma de pago del socio.
        /// </summary>
        [Required(ErrorMessage = "La forma de pago es obligatoria.")]
        public FormaDePago? FormaPago { get; set; }

        // --- Datos personales ---

        /// <summary>
        /// Documento o CUIT del socio. 
        /// Documento para personas y CUIT para empresas.
        /// Debe ser único.
        /// </summary>
        public string? DocumentoOCUIT { get; set; }

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
        /// Representa la dirección del socio.
        /// </summary>
        [Required(ErrorMessage = "la dirección del socio es obligatoria.")]
        public string Direccion { get; set; } = string.Empty;

        /// <summary>
        /// Representa la Latitud del socio. (Dirección GPS)
        /// </summary>
        [Required]
        public double Latitud { get; set; }

        /// <summary>
        /// Representa la Longitud del socio. (Dirección GPS)
        /// </summary>
        [Required]
        public double Longitud { get; set; }

        /// <summary>
        /// Representa la zona del socio.
        /// </summary>
        [Required(ErrorMessage = "La zona del socio es obligatoria.")]
        public Zona? Zona { get; set; }

        /// <summary>
        /// Representa la ocupación del socio.
        /// </summary>
        [StringLength(255, ErrorMessage = "La ocupacion no puede superar los 255 caracteres.")]
        public string? Ocupacion { get; set; }

        /// <summary>
        /// Representa el número de teléfono del socio.
        /// </summary>
        [StringLength(255, ErrorMessage = "El número de telefono no puede superar los 255 caracteres.")]
        [Phone(ErrorMessage = "El número de telefono no tiene un formato válido.")]
        public string Telefono { get; set; } = string.Empty;

        /// <summary>
        /// Representa el correo electrónico del socio.
        /// </summary>
        [StringLength(255, ErrorMessage = "El email no puede superar los 255 caracteres.")]
        [EmailAddress(ErrorMessage = "El email no tiene un formato válido.")]
        public string? Email { get; set; }

        /// <summary>
        /// Pagos realizados por este socio.
        /// Incluye pagos en efectivo y pagos electrónicos.
        /// </summary>
        public List<PagoSocio> Pagos { get; set; } = new();
    }
}