using System.ComponentModel.DataAnnotations;
using FireForce.Core.Data.Models.Salidas.Planillas.Servicios;

namespace FireForce.Core.Data.Models.Salidas.Planillas
{
    public class ServicioEspecialColaboraciónFuerzasSeguridad : ServicioEspecial
    {
        [StringLength(255)]
        public string? DetallesColaboFuerzasSeguridad { get; set; }
    }
}
