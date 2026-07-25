using System.ComponentModel.DataAnnotations;

namespace FireForce.Shared.Enums.Firmas
{
    public enum CategoriaIncidente
    {
        [Display(Name = "Novedad")] // No otorga puntos
        Novedad = 0,

        [Display(Name = "Gestión")] // Firma (otorga puntos)
        Gestion = 1,

        [Display(Name = "Limpieza")] // Firma (otorga puntos)
        Limpieza = 2,

        [Display(Name = "Mantenimiento")] // Firma (otorga puntos)
        Mantenimiento = 3,

        [Display(Name = "Puesta en marcha")] // Firma (otorga puntos)
        PuestaEnMarcha = 4
    }
}
