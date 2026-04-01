using System.ComponentModel.DataAnnotations;

namespace Vista.Data.Models.Salidas.Planillas.Servicios
{
    public class ServicioEspecialColocaciónDriza : ServicioEspecial
    {
        [Required, StringLength(255)]
        public string TipoLugar { get; set; }
        [Required, StringLength(255)]
        public string NombreEstablecimiento { get; set; }
        [StringLength(255)]
        public string? Detalles { get; set; }
    }
}
