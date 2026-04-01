using System.ComponentModel.DataAnnotations;

namespace Vista.Data.Models.Salidas.Planillas.Servicios
{
    public class ServicioEspecialSuministroAgua : ServicioEspecial
    {
        [Required, StringLength(255)]
        public string NombreEstablecimientoSuministroAgua { get; set; }
        [StringLength(255)]
        public string? DetallesSuministroAgua { get; set; }
    }
}
