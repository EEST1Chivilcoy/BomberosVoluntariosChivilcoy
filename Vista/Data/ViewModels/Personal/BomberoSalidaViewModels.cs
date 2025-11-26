using Vista.Data.Enums;
using Vista.Data.Models.Vehiculos.Flota;

namespace Vista.Data.ViewModels.Personal
{
    public class BomberoSalidaViewModels
    {

        public Movil? MovilAsignado { get; set; }
        public EscalafonJerarquico Grado { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int NumeroLegajo { get; set; }
        public int BomberoId { get; set; }
        public string ApellidoYNombre
        {
            get { return Apellido + "," + Nombre; }
        }
    }
}
