using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Vista.Data.Enums;
using Vista.Data.Models.Grupos.FuerzasIntervinientes;
using Vista.Data.Models.Personas.Personal;
using Vista.Data.Models.Vehiculos.Flota;

namespace Vista.Data.Models.Grupos.Dependencias.EquiposAutonomos
{
    /// <summary>
    /// Representa un registro de movimiento de un equipo autónomo, incluyendo cambios de estado y fechas de movimiento.
    /// </summary>
    public class Movimiento_EquipoAutonomo
    {
        /// <summary>
        /// Identificador único del movimiento del equipo autónomo.
        /// </summary>
        public int Movimiento_EquipoAutonomoId { get; set; }

        /// <summary>
        /// Identificador del equipo autónomo asociado al movimiento.
        /// </summary>
        public int EquipoAutonomoId { get; set; }

        /// <summary>
        /// Equipo autónomo asociado al movimiento.
        /// </summary>
        [ForeignKey(nameof(EquipoAutonomoId))]
        public EquipoAutonomo EquipoAutonomo { get; set; } = null!;

        /// <summary>
        /// Fecha del movimiento del equipo autónomo.
        /// </summary>
        public DateTime FechaMovimiento { get; set; }

        /// <summary>
        /// Identificador del encargado del movimiento del equipo autónomo.
        /// </summary>
        public int EncargadoId { get; set; }

        /// <summary>
        /// Encargado del movimiento del equipo autónomo.
        /// </summary>
        [ForeignKey(nameof(EncargadoId))]
        public Bombero Encargado { get; set; } = null!;

        /// <summary>
        /// Estado anterior del equipo autónomo antes del movimiento.
        /// </summary>
        public TipoEstadoEquipoAutonomo EstadoAnterior { get; set; }

        /// <summary>
        /// Estado nuevo del equipo autónomo después del movimiento.
        /// </summary>
        public TipoEstadoEquipoAutonomo EstadoNuevo { get; set; }

        /// <summary>
        /// Agente anterior asociado al movimiento, si aplica.
        /// </summary>
        public string? AgenteAnterior { get; set; } = null;

        /// <summary>
        /// Identificador opcional del vehículo de salida asociado al movimiento, si aplica.
        /// </summary>
        public int? VehiculoDestinoId { get; set; } = null;

        /// <summary>
        /// Propiedad opcional para la relación con el vehículo de salida, si aplica.
        /// </summary>
        [ForeignKey(nameof(VehiculoDestinoId))]
        public VehiculoSalida? VehiculoDestino { get; set; } = null;

        /// <summary>
        /// Identificador opcional de la dependencia asociada al movimiento, si aplica.
        /// </summary>
        public int? DependenciaDestinoId { get; set; } = null;

        /// <summary>
        /// Propiedad opcional para la relación con la dependencia, si aplica.
        /// </summary>
        [ForeignKey(nameof(DependenciaDestinoId))]
        public Dependencia? DependenciaDestino { get; set; } = null;

        /// <summary>
        /// Si el destino no es un vehículo ni una dependencia registrada, se puede especificar aquí.
        /// </summary>
        public string? OtroDestino { get; set; } = null;

        /// <summary>
        /// Si el equipo autónomo fue devuelto al stock luego de una prueba hidráulica exitosa. Se registra la fecha de vencimiento (próxima prueba hidráulica).
        /// </summary>
        [NotMapped]
        public DateTime? FechaVencimientoPruebaHidraulica { get; set; } = null;
    }
}
