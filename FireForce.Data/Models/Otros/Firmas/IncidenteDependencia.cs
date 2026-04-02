using FireForce.Data.Models.Grupos.Dependencias;

namespace FireForce.Data.Models.Otros.Firmas
{
    public class IncidenteDependencia : IncidenteBase
    {
        public Dependencia Dependencia { get; set; } = null!;
        public int DependenciaId { get; set; }
    }
}
