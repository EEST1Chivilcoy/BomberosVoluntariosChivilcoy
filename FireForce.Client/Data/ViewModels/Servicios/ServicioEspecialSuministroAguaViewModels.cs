using System.ComponentModel.DataAnnotations;

namespace FireForce.Client.Data.ViewModels.Servicios
{
    public class ServicioEspecialSuministroAguaViewModels : ServicioEspecialViewModel
    {
        [StringLength(255)]
        public string? NombreEstablecimientoSuministroAgua { get; set; }
        [StringLength(255)]
        public string? DetallesSuministroAgua { get; set; }
    }
}
