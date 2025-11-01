using System.ComponentModel.DataAnnotations;

namespace Vista.Data.ViewModels.Servicios
{
    public class ServicioEspecialColaboraciónFuerzasSeguridadViewModels : ServicioEspecialViewModel
    {
        [StringLength(255)]
        public string? DetallesColaboFuerzasSeguridad { get; set; }
    }
}
