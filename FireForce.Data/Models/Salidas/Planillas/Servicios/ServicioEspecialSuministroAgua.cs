using System.ComponentModel.DataAnnotations;

namespace FireForce.Data.Models.Salidas.Planillas.Servicios
{
    public class ServicioEspecialSuministroAgua : ServicioEspecial
    {
        [StringLength(255)]
        public string? NombreEstablecimientoSuministroAgua { get; set; }
        [StringLength(255)]
        public string? DetallesSuministroAgua { get; set; }
    }
}
