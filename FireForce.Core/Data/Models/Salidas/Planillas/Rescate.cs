using System.ComponentModel.DataAnnotations;

namespace Vista.Data.Models.Salidas.Planillas
{
    public abstract class Rescate : Salida
    {
        //Localización, Datos del solicitante, personas damnificadas y datos del seguro
        [StringLength(255)]
        public string? OtroLugarRescate { get; set; }
    }
}
