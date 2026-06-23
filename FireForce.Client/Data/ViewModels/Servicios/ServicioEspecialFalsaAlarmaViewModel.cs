using System.ComponentModel.DataAnnotations;

namespace FireForce.Client.Data.ViewModels.Servicios
{
    public class ServicioEspecialFalsaAlarmaViewModel : ServicioEspecialViewModel
    {
        [StringLength(255)]
        public string Detalles { get; set; }
    }
}
