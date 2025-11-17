using System.ComponentModel.DataAnnotations;

namespace Vista.Data.Enums
{
    public enum TipoLugarComercioIncendio
    {
        [Display(Name = "Bar")]
        Bar,

        [Display(Name = "Restaurante (Local de Comida)")]
        RestauranteLocalDeComida,

        [Display(Name = "Cafetería")]
        Cafeteria,

        [Display(Name = "Panadería / Confitería")]
        PanaderiaConfiteria,

        [Display(Name = "Kiosco / Almacén")]
        KioscoAlmacen,

        [Display(Name = "Supermercado")]
        Supermercado,

        [Display(Name = "Hipermercado")]
        Hipermercado,

        [Display(Name = "Shopping")]
        Shopping,

        [Display(Name = "Galería comercial")]
        GaleriaComercial,

        [Display(Name = "Peluquería / Estética")]
        PeluqueriaEstetica,

        [Display(Name = "Tienda de ropa / calzado")]
        TiendaRopaCalzado,

        [Display(Name = "Ferretería / Corralón")]
        FerreteriaCorralon,

        [Display(Name = "Electrónica / Electrodomésticos")]
        ElectronicaElectrodomesticos,

        [Display(Name = "Librería / Papelería")]
        LibreriaPapeleria,

        [Display(Name = "Farmacia")]
        Farmacia,

        [Display(Name = "Veterinaria / Pet Shop")]
        VeterinariaPetShop,

        [Display(Name = "Teatro")]
        Teatro,

        [Display(Name = "Cine")]
        Cine,

        [Display(Name = "Gimnasio / Centro deportivo")]
        GimnasioCentroDeportivo,

        [Display(Name = "Salón de eventos / Fiestas")]
        SalonEventosFiestas,

        [Display(Name = "Oficina comercial / Agencia")]
        OficinaComercialAgencia,

        [Display(Name = "Estación de servicio")]
        EstacionDeServicio,

        [Display(Name = "Otro")]
        Otro
    }
}