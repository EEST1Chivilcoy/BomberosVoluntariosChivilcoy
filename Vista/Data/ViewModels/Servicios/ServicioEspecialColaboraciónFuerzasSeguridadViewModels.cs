using System.ComponentModel.DataAnnotations;

namespace Vista.Data.ViewModels.Servicios
{
    public class ServicioEspecialColaboraciónFuerzasSeguridadViewModels : ServicioEspecialViewModel
    {
        [Required]
        public List<int> ColaboracionFuerzasSeguridadIds { get; set; } = new List<int>();

        [StringLength(255)]
        public string? DetallesColaboFuerzasSeguridad { get; set; }
    }
}
