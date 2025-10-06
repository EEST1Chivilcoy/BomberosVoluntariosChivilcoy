using System.ComponentModel.DataAnnotations;
using Vista.Data.Models.Personas;

namespace Vista.Data.ViewModels.Personal
{
    public class VehiculoDamnificadoViewModels
    {
        [StringLength(255)]
        public string? Marca { get; set; }
        [StringLength(255)]
        public string? Modelo { get; set; }
        public int? Año { get; set; }
        [StringLength(255)]
        public string? Patente { get; set; }
        [StringLength(255)]
        public string Tipo { get; set; }

        [StringLength(255)]
        public string? Color { get; set; }
        public bool Airbag { get; set; }
        public string? Observaciones { get; set; }
        public bool seConoceConductor { get; set; }
        public string? Dueño { get; set; }
        public string CompañiaAseguradora { get; set; }
        [Required, StringLength(255)]
        public string NumeroDePoliza { get; set; }
        public DateTime? FechaDeVencimiento { get; set; }
        public int? DamnificadoId { get; set; }
        public Damnificado_Salida? Conductor { get; set; }
        public IEnumerable<Damnificado_Salida> Pasajeros { get; set; } = new List<Damnificado_Salida>();
    }
}
