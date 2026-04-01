namespace Vista.Data.Enums;
using System.ComponentModel.DataAnnotations;

public enum EscalafonJerarquico
{
    [Display(Name = "Jefe")]
    Jefe = 1,
    [Display(Name = "2° Jefe")]
    Segundojefe = 2,
    [Display(Name = "1° Oficial")]
    PrimerOficial = 3,
    [Display(Name = "2° Ofical")]
    SegundoOficial = 4,
    [Display(Name = "3° Oficial")]
    TercerOficial = 5,
    [Display(Name = "Auxiliar Mayor")]
    AuxiliarMayor = 6,
    [Display(Name = "1° Auxiliar")]
    PrimerAuxiliar = 7,
    [Display(Name = "2° Auxiliar")]
    SegundoAuxiliar = 8,
    [Display(Name = "1° Ayudante")]
    PrimerAyudante = 9,
    [Display(Name = "2° Ayudante")]
    SegundoAyudante = 10,
    [Display(Name = "Bombero")]
    Bombero = 11,
    [Display(Name = "Aspirante")]
    Aspirante = 12
}

