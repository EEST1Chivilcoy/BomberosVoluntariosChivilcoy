using System.ComponentModel.DataAnnotations;

namespace Vista.Data.Models.Salidas.Planillas.Servicios
{
    public class ServicioEspecialFalsaAlarma : ServicioEspecial
    {
        [Required, StringLength(255)]
        public string Detalles { get; set; }

    }
}
