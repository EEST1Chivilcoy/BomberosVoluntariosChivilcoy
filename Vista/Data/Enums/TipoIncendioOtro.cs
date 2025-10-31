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
        [Display(Name = "Lancha")]
        Lancha,
        [Display(Name = "Yate")]
        Yate,
        [Display(Name = "Barco Pesquero")]
        BarcoPesquero,
        [Display(Name = "Buque de Carga")]
        BuqueDeCarga,
        [Display(Name = "Crucero")]
        Crucero,
        [Display(Name = "Moto de Agua")]
        MotoDeAgua,
        [Display(Name = "Otro")]
        Otro
    }
}
