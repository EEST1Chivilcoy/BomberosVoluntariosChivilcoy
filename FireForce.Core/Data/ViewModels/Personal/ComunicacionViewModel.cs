using System.ComponentModel.DataAnnotations;
using FireForce.Core.Data.Enums;
using FireForce.Core.Data.Models.Personas.Personal;
using FireForce.Core.Data.Models.Salidas.Componentes;
using FireForce.Core.Data.Models.Salidas.Planillas;
using FireForce.Core.Data.Models.Vehiculos.Flota;

namespace FireForce.Core.Data.ViewModels.Personal
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
