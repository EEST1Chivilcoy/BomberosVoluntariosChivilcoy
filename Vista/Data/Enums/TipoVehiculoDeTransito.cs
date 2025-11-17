using System.ComponentModel.DataAnnotations;

namespace Vista.Data.Enums
{
    public enum TipoVehiculoDeTransito
    {
        [Display(Name = "Automóvil")]
        Automovil,

        [Display(Name = "Motocicleta / ciclomotor")]
        Motocicleta,

        [Display(Name = "Bicicleta")]
        Bicicleta,

        [Display(Name = "Camioneta / Pick-up")]
        Camioneta,

        [Display(Name = "Camión")]
        Camion,

        [Display(Name = "Colectivo urbano")]
        ColectivoUrbano,

        [Display(Name = "Micro / larga distancia")]
        MicroLargaDistancia,

        [Display(Name = "Remís / taxi")]
        RemisTaxi,

        [Display(Name = "Cuatriciclo")]
        Cuatriciclo,

        [Display(Name = "Patineta / monopatín eléctrico")]
        MonopatinElectrico,

        [Display(Name = "Ambulancia / unidad sanitaria")]
        Ambulancia,

        [Display(Name = "Patrullero / móvil policial")]
        Patrullero,

        [Display(Name = "Unidad de bomberos / emergencia")]
        UnidadEmergencia,

        [Display(Name = "Otro")]
        Otro
    }
}