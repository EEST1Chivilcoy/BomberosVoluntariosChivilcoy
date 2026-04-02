using FireForce.Data.Models.Personas.Personal;
using FireForce.Data.Models.Vehiculos.Flota;
using FireForce.Shared.Enums;

namespace FireForce.Client.Data.ViewModels.Personal
{
    public class ComunicacionViewModel
    {
        public string NroEquipo { get; set; }
        public string? Marca { get; set; }
        public string? Modelo { get; set; }
        public string? NroSerie { get; set; }
        public TipoEstadoHandie Estado { get; set; }
        public Bombero? Bombero { get; set; }
        public Movil? Movil { get; set; }
        public string NombreYApellido
        {
            get { return Bombero.Nombre + "," + Bombero.Apellido; }
        }
    }
}
