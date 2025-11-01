using System.ComponentModel.DataAnnotations;

namespace Vista.Data.ViewModels.Servicios
{
    public class ServicioEspecialFalsaAlarmaViewModel : ServicioEspecialViewModel
    {
        [Required, StringLength(255)]
        public string Detalles { get; set; }
    }
}
