using System.ComponentModel.DataAnnotations;

namespace FireForce.Data.Models.Salidas.Planillas.Servicios
{
    public class ServicioEspecialColaboraciónFuerzasSeguridad : ServicioEspecial
    {
        [StringLength(255)]
        public string? DetallesColaboFuerzasSeguridad { get; set; }
    }
}
