using Vista.Data.Models.Grupos.Dependencias;

namespace Vista.Data.Models.Otros.Firmas
{
    public class IncidenteDependencia : IncidenteBase
    {
        public Dependencia Dependencia { get; set; } = null!;
        public int DependenciaId { get; set; }
    }
}
