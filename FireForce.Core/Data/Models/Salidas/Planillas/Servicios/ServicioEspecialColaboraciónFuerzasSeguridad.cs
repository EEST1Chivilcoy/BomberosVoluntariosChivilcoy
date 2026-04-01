using System.ComponentModel.DataAnnotations;
using Vista.Data.Models.Salidas.Planillas.Servicios;

namespace Vista.Data.Models.Salidas.Planillas
{
    public class ServicioEspecialColaboraciónFuerzasSeguridad : ServicioEspecial
    {
        [StringLength(255)]
        public string? DetallesColaboFuerzasSeguridad { get; set; }
    }
}
