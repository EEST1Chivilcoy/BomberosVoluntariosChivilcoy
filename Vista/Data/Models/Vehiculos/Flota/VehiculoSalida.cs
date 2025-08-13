using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Vista.Data.Enums;
using Vista.Data.Models.Imagenes;
using Vista.Data.Models.Vehiculos;
using Vista.Data.Models.Personas.Personal;
using Vista.Data.Models.Personas.Personal.Componentes;
using Vista.Data.Models.Vehiculos.Flota.Componentes;
using Vista.Data.Models.Otros;

namespace Vista.Data.Models.Vehiculos.Flota
{
    public abstract class VehiculoSalida : Vehiculo
    {
        [StringLength(255)]
        public string? NumeroMovil { get; set; }
        public int? EncargadoId { get; set; }
        [ForeignKey("EncargadoId")]
        public Bombero? Encargado { get; set; }
        public TipoEstadoMovil Estado { get; set; }
        public List<Firma> Firmas { get; set; } = new();
        public List<Incidente> Incidentes { get; set; } = new();
        public int? ImagenId { get; set; }
        public Imagen_VehiculoSalida? Imagen { get; set; }
        public List<NovedadVehiculo>? Novedades { get; set; }

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

        public string? Observaciones { get; set; }
    }
}
