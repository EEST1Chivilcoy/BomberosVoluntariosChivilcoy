using Vista.Data.Enums.Socios;

namespace Vista.Pages.Socios.Cobranza
{
    /// <summary>
    /// ViewModel para la página de cobranza de socios.
    /// </summary>
    public class CobranzaViewModel
    {
        public int SocioId { get; set; }
        public int NumeroSocio { get; set; }
        public string? NombreApellidoONombreEmpresa { get; set; }
        public string? DocumentoORazonSocial { get; set; }
        public string? Domicilio { get; set; }
        public string? DomicilioSecundario { get; set; }
        public decimal Saldo { get; set; }
        public decimal Latitud { get; set; }
        public decimal Longitud { get; set; }
        public decimal? LatitudSecundaria { get; set; }
        public decimal? LongitudSecundaria { get; set; }
        public string? Telefono { get; set; }
        public decimal MontoCuota { get; set; }
        public Zona? Zona { get; set; }
    }
}
