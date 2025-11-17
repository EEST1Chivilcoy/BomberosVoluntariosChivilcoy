using System.ComponentModel.DataAnnotations;

namespace Vista.Data.Enums.Socios
{
    /// <summary>
    /// Representa los bancos argentinos conocidos / más usados para pagos electrónicos.
    /// Esto en la fecha del 13 de noviembre de 2025. (Este enum puede crecer con el tiempo) (Fijarse en algun futuro, de migrarlo a una tabla en la base de datos)
    /// </summary>
    public enum BancosConocidos
    {
        // Bancos tradicionales
        [Display(Name = "Banco Nación (BNA+)")]
        Nacion = 1,

        [Display(Name = "Banco Galicia")]
        Galicia = 2,

        [Display(Name = "Banco Santander")]
        Santander = 3,

        [Display(Name = "BBVA")]
        BBVA = 4,

        [Display(Name = "Banco Macro")]
        Macro = 5,

        [Display(Name = "Banco Provincia de Buenos Aires (Cuenta DNI)")]
        ProvinciaDeBuenosAires = 6,

        [Display(Name = "Banco Credicoop")]
        Credicoop = 7,

        [Display(Name = "Banco Ciudad")]
        CiudadDeBuenosAires = 8,

        [Display(Name = "Banco Patagonia")]
        Patagonia = 9,

        [Display(Name = "Banco Supervielle")]
        Supervielle = 10,

        [Display(Name = "Banco Hipotecario")]
        Hipotecario = 11,

        [Display(Name = "Banco Comafi")]
        Comafi = 12,

        [Display(Name = "HSBC Argentina")]
        HSBC = 13,

        [Display(Name = "Banco Itaú")]
        Itau = 14,

        [Display(Name = "Banco Industrial")]
        Industrial = 15,

        [Display(Name = "BICE")]
        BICE = 16,

        // Cooperativas / regionales
        [Display(Name = "Cooperativa Chivilcoy")]
        ChivilcoyCoop = 17,

        // Billeteras virtuales con CVU
        [Display(Name = "Mercado Pago")]
        MercadoPago = 100,

        [Display(Name = "Ualá")]
        Uala = 101,

        [Display(Name = "Naranja X")]
        NaranjaX = 102,

        [Display(Name = "Personal Pay")]
        PersonalPay = 103,

        [Display(Name = "Modo")]
        Modo = 104,

        [Display(Name = "Prex")]
        Prex = 105,

        // Opción genérica
        [Display(Name = "Otro")]
        Otro = 999
    }
}