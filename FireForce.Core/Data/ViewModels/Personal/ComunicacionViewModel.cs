using System.ComponentModel.DataAnnotations;
using Vista.Data.Enums;
using Vista.Data.Models.Personas.Personal;
using Vista.Data.Models.Salidas.Componentes;
using Vista.Data.Models.Salidas.Planillas;
using Vista.Data.Models.Vehiculos.Flota;

namespace Vista.Data.ViewModels.Personal
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
