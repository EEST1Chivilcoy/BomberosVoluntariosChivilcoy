using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Vista.Data.Enums;
using Vista.Data.Models.Imagenes;
using Vista.Data.Models.Vehiculos;
using Vista.Data.Models.Personas.Personal;
using Vista.Data.Models.Personas.Personal.Componentes;

namespace Vista.Data.Models.Vehiculos.Flota
{
    public abstract class VehiculoSalida : Vehiculo
    {
        /// <summary>
        /// Numero movil del vehiculo.
        /// Puede ser tambien un identificador unico para el vehiculo dentro de la flota.
        /// Este campo es obligatorio y debe ser unico.
        /// Se asigna a las embarcaciones, y a los vehiculos (moviles) de la institucion.
        /// </summary>
        [Required(ErrorMessage = "El número de móvil es obligatorio.")]
        [StringLength(255, ErrorMessage = "El número de móvil no puede superar los 255 caracteres.")]
        public string? NumeroMovil { get; set; }

        /// <summary>
        /// Foreanea para el bombero encargado del vehículo.
        /// </summary>
        public int? EncargadoId { get; set; }

        /// <summary>
        /// Bombero encargado del vehículo.
        /// </summary>
        [ForeignKey("EncargadoId")]
        public Bombero? Encargado { get; set; }

        /// <summary>
        /// Estado del vehículo móvil.
        /// (Por ejemplo, Activo, Inactivo, En Mantenimiento, etc.)
        /// </summary>
        public TipoEstadoMovil Estado { get; set; }

        /// <summary>
        /// Firmas asociadas al vehículo.
        /// </summary>
        public List<Firma> Firmas { get; set; } = new();


        /// <summary>
        /// Foranea para la imagen del vehículo.
        /// </summary>
        public int? ImagenId { get; set; }

        /// <summary>
        /// Imagen asociada al vehículo.
        /// </summary>
        public Imagen_VehiculoSalida? Imagen { get; set; }


        /// <summary>
        /// Tipo de combustible (por ejemplo, Diesel). Con un maximo de 255 caracteres.
        /// </summary>
        [StringLength(255)]
        public string? Combustible { get; set; }

        /// <summary>
        /// Fecha de realizacion del ultimo service al vehiculo.
        /// </summary>
        public DateTime? FechaUltimoService { get; set; }

        /// <summary>
        /// Fecha en la que se realizara un service al vehiculo
        /// </summary>
        public DateTime? FechaProximoService { get; set; }

        /// <summary>
        /// Observaciones adicionales sobre el vehículo.
        /// </summary>
        public string? Observaciones { get; set; }
    }
}
