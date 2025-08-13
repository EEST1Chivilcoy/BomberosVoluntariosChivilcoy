using Vista.Data.Enums;
using Vista.Data.Models.Salidas.Componentes;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Vista.Data.Models.Grupos.Dependencias.Comunicaciones;
using Vista.Data.Enums.Discriminadores;

namespace Vista.Data.Models.Vehiculos.Flota
{
    /// <summary>
    /// Clase que representa un vehículo móvil de la institución.
    /// </summary>
    [Index(nameof(NumeroMovil))]
    public class Movil : VehiculoSalida
    {
        /// <summary>
        /// Número de motor del vehículo móvil. 
        /// Este campo es opcional y puede ser nulo.
        /// </summary>
        [StringLength(255)]
        public string? NumeroMotor { get; set; }

        /// <summary>
        /// Modelo de la bomba del vehículo móvil (por ejemplo, tipo de bomba utilizada en el camión de bomberos).
        /// Este campo es opcional.
        /// </summary>
        [StringLength(255)]
        public string? ModeloBomba { get; set; }

        /// <summary>
        /// Número de chasis del vehículo móvil.
        /// Este campo es opcional y puede ser nulo.
        /// </summary>
        [StringLength(255)]
        public string? NumeroChasis { get; set; }

        /// <summary>
        /// Cantidad de litros que puede contener el tanque de agua o combustible del vehículo móvil.
        /// Este campo es opcional.
        /// </summary>
        [StringLength(255)]
        public int? CantidadLitros { get; set; }

        /// <summary>
        /// Tipo de Aceite (Mineral, Sintetico)
        /// </summary>
        [StringLength(255)]
        public string? TipoAceite { get; set; }

        /// <summary>
        /// Marca del Aceite.
        /// </summary>
        [StringLength(255)]
        public string? MarcaAceite { get; set; }

        /// <summary>
        /// Cantidad de Litros de Aceite.
        /// </summary>
        public int? CantidadAceite { get; set; }

        /// <summary>
        /// Modelo del filtro de aire
        /// </summary>
        [StringLength(255)]
        public string? ModeloFiltroAire { get; set; }

        /// <summary>
        /// Medidas de las cubiertas.
        /// Este campo es de tipo String
        /// </summary>
        [StringLength(255)]
        public string? MedidasCubiertas { get; set; }

        /// <summary>
        /// Libras de las Cubiertas, en PSI
        /// </summary>
        [StringLength(255)]
        public string? LibrasCubiertas { get; set; }
        
        /// <summary>
        /// Tension del Circuito Electrico, 12V o 24v.
        /// Trabaja con un Enum.
        /// </summary>
        public TipoTension? TensionCElectrico { get; set; }

        /// <summary>
        /// Tipo de direccion.
        /// Trabaja con un Enum.
        /// </summary>
        public TipoDireccionUnidades? TipoDireccion { get; set; }

        /// <summary>
        /// Tipo de Cajas de Velocidades.
        /// Trabaja con un Enum.
        /// </summary>
        public TipoCajaVelocidades? CajaVelocidades { get; set; }

        /// <summary>
        /// Marca de la Bateria.
        /// </summary>
        [StringLength(255)]
        public string? MarcaBateria { get; set; }

        /// <summary>
        /// Fecha del Ultimo cambio de bateria.
        /// </summary>
        public DateTime? FechaUltCambioBateria { get; set; }

        /// <summary>
        /// Kilometraje actual del vehículo móvil.
        /// </summary>
        public int Kilometraje { get; set; }

        /// <summary>
        /// Lista de salidas o misiones en las que el vehículo móvil ha participado.
        /// Es una lista que contiene las salidas registradas del vehículo.
        /// </summary>
        public List<Movil_Salida> Salidas { get; set; } = new();

        /// <summary>
        /// Identificador del equipo de comunicación (Handie) asociado a este vehículo.
        /// Este campo es opcional y puede ser nulo si el vehículo no está asociado a un equipo.
        /// </summary>
        public int? EquipoId { get; set; }

        /// <summary>
        /// Información del equipo de comunicación (Handie) del vehículo móvil.
        /// Es una relación opcional con un objeto `Comunicacion`.
        /// </summary>
        public Comunicacion? HandieMovil { get; set; }
    }
}
