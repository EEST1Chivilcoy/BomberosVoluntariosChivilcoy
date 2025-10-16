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
        /// Propiedad opcional para la relación con el vehículo de salida, si aplica.
        /// </summary>
        public VehiculoSalida? VehiculoAgente { get; set; } = null;

        /// <summary>
        /// Propiedad opcional para la relación con la dependencia, si aplica.
        /// </summary>
        public Dependencia? DependenciaAgente { get; set; } = null;
    }
}
