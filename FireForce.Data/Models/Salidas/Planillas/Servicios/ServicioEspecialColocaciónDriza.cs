using System.ComponentModel.DataAnnotations;

namespace FireForce.Data.Models.Salidas.Planillas.Servicios
{
    public class ServicioEspecialColocaciónDriza : ServicioEspecial
    {
        [StringLength(255)]
        public string TipoLugar { get; set; }
        [StringLength(255)]
        public string NombreEstablecimiento { get; set; }
        [StringLength(255)]
        public string? Detalles { get; set; }
    }
}
