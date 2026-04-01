using Vista.Data.Models.Personas.Personal;
using Vista.Data.Models.Vehiculos.Flota;

namespace Vista.Data.ViewModels.Personal
{
    public class MovilSalidaViewModels
    {
        public bool CargoCombustible { get; set; }
        public string? NumeroFactura { get; set; }
        public DateTime? FechaFactura { get; set; }
        public string? TipoConbustible { get; set; }
        public string? CantidadLitros { get; set; }
        public BomberoViweModel? QuienLleno { get; set; }
        public string? TelefonoQuienLleno { get; set; }
        public BomberoViweModel Chofer { get; set; } = null!;
        public string NumeroMovil { get; set; } = null!;
        public int KmSalida { get; set; }
        public int KmLlegada { get; set; }
        public string ChoferNombreCompleto { get { return Chofer.Nombre + "," + Chofer.Apellido; } }

    }
}
