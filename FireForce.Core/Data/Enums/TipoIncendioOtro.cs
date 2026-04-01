using System.ComponentModel.DataAnnotations;

namespace Vista.Data.Enums
{
    public enum TipoIncendioOtro
    {
        [Display(Name = "Tendido eléctrico")]
        TendidoElectrico,

        [Display(Name = "Medidor eléctrico")]
        MedidorElectrico,

        [Display(Name = "Transformador / Subestación")]
        TransformadorSubestacion,

        [Display(Name = "Poste / Caja de distribución")]
        PosteCajaDistribucion,

        [Display(Name = "Contenedor")]
        ContenedorResiduos,

        [Display(Name = "Montículo de basura / poda")]
        MonticuloBasuraPoda,

        [Display(Name = "Cámara cloacal / pluvial")]
        CamaraCloacalPluvial,

        [Display(Name = "Vehículo abandonado")]
        VehiculoAbandonadoDesguazado,

        [Display(Name = "Incendio en vía pública")]
        IncendioViaPublica,

        [Display(Name = "Incendio en baldío / terreno urbano")]
        IncendioBaldioTerrenoUrbano,

        [Display(Name = "Otro")]
        Otro
    }
}