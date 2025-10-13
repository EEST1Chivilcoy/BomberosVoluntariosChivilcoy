using Vista.Data.Enums;
using Vista.Data.Models.Vehiculos.Flota;

namespace Vista.Data.Models.Grupos.Dependencias.EquiposAutonomos
{
    public class EquipoAutonomo
    {
        public int Id { get; set; }
        public string NroSerie { get; set; }
        public string? NroTubo { get; set; }
        public string? Marca { get; set; }
        public string? Modelo { get; set; }
        public string? TipoMaterial { get; set; }
        public TipoEstadoEquipoAutonomo Estado { get; set; } = TipoEstadoEquipoAutonomo.Stock;
        public DateTime? FechaInicioPrueba { get; set; }
        public DateTime? FechaFinPrueba { get; set; }

        //Relacion con un Movil/Embarcacion
        public int? VehiculoSalidaId { get; set; }
        public VehiculoSalida? VehiculoSalida { get; set; }

        //Relacion con una Dependencia
        public int? DependenciaId { get; set; }
        public Dependencia? Dependencia { get; set; }
    }
}
