using Vista.Data.Enums;
using Vista.Data.Models.Salidas.Componentes;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Vista.Data.Models.Grupos.Dependencias.Comunicaciones;

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
