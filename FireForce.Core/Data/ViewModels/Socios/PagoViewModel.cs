using Vista.Data.Enums.Discriminadores;
using Vista.Data.Enums.Socios;

namespace Vista.Data.ViewModels.Socios
{
    public class PagoViewModel
    {
        public int Id { get; set; }
        public int SocioId { get; set; }
        public string NombreSocio { get; set; } = string.Empty;
        public int NumeroSocio { get; set; }
        public DateTime Fecha { get; set; }
        public DateTime? FechaConfirmadoORechazado { get; set; }
        public string? RazonRechazo { get; set; }
        public double Monto { get; set; }
        public TipoPagoSocio Tipo { get; set; }
        public EstadoPago Estado { get; set; }
        public int CobradorId { get; set; }
        public string? NombreCobrador { get; set; }
        public string? Observaciones { get; set; }
    }
}
