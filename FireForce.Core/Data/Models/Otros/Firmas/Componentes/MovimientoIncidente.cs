using Vista.Data.Models.Personas.Personal;

namespace Vista.Data.Models.Otros.Firmas.Componentes
{
    public class MovimientoIncidente
    {
        public int Id { get; set; }
        public int EncargadoId { get; set; }
        public Bombero Encargado { get; set; } = null!;
        public DateTime Fecha { get; set; } = DateTime.Now;
        public string? Descripcion { get; set; }
        public int IncidenteId { get; set; }
        public IncidenteBase Incidente { get; set; }
    }
}
