using FireForce.Core.Data.Enums;

namespace FireForce.Core.Pages.Vehiculos.ViewModel
{
    public class UnidadSimpleViewModel
    {
        public int Id { get; set; }
        public string NumeroMovil { get; set; } = string.Empty;
        public string? ImageUrl { get; set; } = string.Empty;
        public TipoEstadoMovil Estado { get; set; }
    }
}
