using System.ComponentModel.DataAnnotations;

namespace FireForce.Data.Models.Salidas.Planillas.Servicios
{
    public class ServicioEspecialFalsaAlarma : ServicioEspecial
    {
        [StringLength(255)]
        public string Detalles { get; set; }

    }
}
