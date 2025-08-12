using Vista.Data.Enums;
using Vista.Data.Models.Salidas.Componentes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Vista.Data.Enums.Salidas;
using Vista.Data.Models.Personas;
using Vista.DTOs.Nominatim;

namespace Vista.Data.ViewModels
{
    public abstract class SalidasViewModels
    {
        public int NumeroParte { get; set; }
        public int AnioNumeroParte { get; set; }

        // Otras propiedades

        //Fechas y Tiempo
        public DateTime FechaSalida { get; set; }
        public DateTime FechaLlegada
        {
            get
            {
                // Si TimeLlegada es menor que TimeSalida, se añade un día a la fecha de salida.
                return TimeLlegada < TimeSalida ? FechaSalida.AddDays(1) : FechaSalida;
            }
        }
        public TimeOnly TimeSalida { get; set; }
        public TimeOnly TimeLlegada { get; set; }

        // HoraSalida - HoraLLegada Completas (Fecha y Hora)
        public DateTime HoraSalida
        {
            get
            {
                // Combina FechaSalida con TimeSalida
                return FechaSalida.Date.Add(TimeSalida.ToTimeSpan());
            }
        }

        public DateTime HoraLLegada
        {
            get
            {
                // Combina FechaLlegada con TimeLlegada
                return FechaLlegada.Date.Add(TimeLlegada.ToTimeSpan());
            }
        }

        public Direccion Direccion { get; set; }


        [Required, StringLength(255)]
        public string Descripcion { get; set; }
        [StringLength(255)]
        public string CalleORuta { get; set; }
        [StringLength(255)]
        public string? PisoNumero { get; set; }
        [StringLength(255)]
        public string? Depto { get; set; }
        public TipoZona TipoZona { get; set; }

        public string NombreSolicitante { get; set; }
        public string ApellidoSolicitante { get; set; }
        public string DniSolicitante { get; set; }
        public string TelefonoSolicitante { get; set; }
        public CuartelRegionChivilcoy CuartelRegion { get; set; }

        public string? NombreYApellidoReceptor
        {
            get
            {
                if (string.IsNullOrEmpty(ApellidoReceptor) && string.IsNullOrEmpty(NombreReceptor))
                    return null;

                // Combina el apellido y el nombre con una coma y un espacio
                return $"{ApellidoReceptor}, {NombreReceptor}";
            }
        }

        public string? NombreReceptor { get; set; }
        public string? ApellidoReceptor { get; set; }
        public int LegajoReceptor { get; set; }

        public List<Damnificado_Salida>? Damnificados { get; set; }
        public List<VehiculoAfectadoIncendio> VehiculosAfectadosIncendios { get; set; }

        //datos del seguro 
        public string? CompaniaAseguradora { get; set; }
        public string? NumeroPoliza { get; set; }
        public DateTime? FechaVencimineto { get; set; }

        public List<Movil_Salida> Moviles { get; set; }
        public List<BomberoSalida> CuerpoParticipante { get; set; }
        //Bombero encargado
        public string NombreEncargado { get; set; }
        public string ApellidoEncargado { get; set; }
        public int LegajoEncargado { get; set; }
        //bombero que lleno la planilla
        public string NombreLLenoPlanilla { get; set; }
        public string ApllidoLLenoPlanilla { get; set; }
        public string NombreYApellidoLlenoPlanilla
        {
            get { return NombreLLenoPlanilla + " " + ApllidoLLenoPlanilla; }
        }
        public int LegajoLLenoPlanilla { get; set; }
        public TipoServicioSalida TipoServicio { get; set; }

        public TipoServicioRepresentaciones TipoRepresentacion { get; set; }
        public Guardia GuardiaSelecionada { get; set; }

        [Required]
        public double Latitud { get; set; }
        [Required]
        public double Longitud { get; set; }
    }
}
