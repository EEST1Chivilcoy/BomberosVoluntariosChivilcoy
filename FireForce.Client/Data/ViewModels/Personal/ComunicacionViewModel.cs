using FireForce.Core.Data.Enums;
using FireForce.Core.Data.Models.Personas.Personal;
using FireForce.Core.Data.Models.Vehiculos.Flota;

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
