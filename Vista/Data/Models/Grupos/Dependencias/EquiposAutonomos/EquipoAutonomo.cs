using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Vista.Data.Enums;
using Vista.Data.Models.Grupos.FuerzasIntervinientes;
using Vista.Data.Models.Vehiculos.Flota;

namespace Vista.Data.Models.Grupos.Dependencias.EquiposAutonomos
{
    /// <summary>
    /// Representa un equipo autónomo utilizado por una dependencia o vehículo de salida.
    /// un equipo autónomo es un equipo de respiración autónoma utilizado por bomberos y personal de emergencia para proporcionar aire respirable en ambientes peligrosos.
    /// </summary>
    public class EquipoAutonomo
    {
        /// <summary>
        /// Identificador único del equipo autónomo.
        /// </summary>
        public int EquipoAutonomoId { get; set; }

        /// <summary>
        /// Número de serie del equipo autónomo. Requerido.
        /// </summary>
        [Required]
        public string NroSerie { get; set; } = null!;

        /// <summary>
        /// Número de tubo del equipo autónomo. Opcional.
        /// </summary>
        public string? NroTubo { get; set; }

        /// <summary>
        /// Marca del equipo autónomo. Opcional.
        /// </summary>
        public string? Marca { get; set; }

        /// <summary>
        /// Modelo del equipo autónomo. Opcional.
        /// </summary>
        public string? Modelo { get; set; }

        /// <summary>
        /// Tipo de material del equipo autónomo. Opcional.
        /// </summary>
        public string? TipoMaterial { get; set; }

        /// <summary>
        /// Tipo de estado del equipo autónomo. Por defecto es 'Stock'.
        /// </summary>
        public TipoEstadoEquipoAutonomo Estado { get; set; } = TipoEstadoEquipoAutonomo.Stock;

        /// <summary>
        /// Movimientos históricos del equipo autónomo.
        /// </summary>
        public List<Movimiento_EquipoAutonomo> Movimientos { get; set; } = new();
    }
}
