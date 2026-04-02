using System.ComponentModel.DataAnnotations;

namespace FireForce.Data.Models.Salidas.Planillas.Servicios
{
    public class ServicioEspecialRetiradoDeObito : ServicioEspecial
    {
        [Required, StringLength(255)]
        public string DetallesObito { get; set; }
    }
}
