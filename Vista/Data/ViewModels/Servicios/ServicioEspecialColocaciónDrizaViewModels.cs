using System.ComponentModel.DataAnnotations;

namespace Vista.Data.ViewModels.Servicios
{
    public class ServicioEspecialColocaciónDrizaViewModels : ServicioEspecialViewModel
    {
        [Required, StringLength(255)]
        public string TipoLugar { get; set; }
        [Required, StringLength(255)]
        public string NombreEstablecimiento { get; set; }
        [StringLength(255)]
        public string? Detalles { get; set; }
    }
}
