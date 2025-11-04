using System.ComponentModel.DataAnnotations;
namespace Vista.Data.Enums
{
    public enum TipoIncendioOtro
    {
        [Display(Name = "Basural")]
        Basural,
        [Display(Name = "Vehículo")]
        Vehiculo,
        [Display(Name = "Tendido Eléctrico")]
        TendidoElectrico,
        [Display(Name = "Contenedor")]
        Contenedor,
        [Display(Name = "Medidor Eléctrico")]
        MedidorEléctrico,   
        [Display(Name = "Otro")]
        Otro
    }
}
