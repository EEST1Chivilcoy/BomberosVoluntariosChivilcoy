using System.ComponentModel.DataAnnotations;
using Vista.Data.Enums;
using Vista.Data.Models.Salidas.Componentes;
using Vista.Data.Models.Salidas.Planillas;

namespace Vista.Data.ViewModels.Personal
{
    public class DamnificadoViewModels 
    {
        [StringLength(255)]
        public string? Nombre { get; set; }
        [StringLength(255)]
        public string? Apellido { get; set; }
        private string _nombreYApellido;
        public string NombreYApellido
        {
            get { return Nombre + "," + Apellido; }
            set
            {
                _nombreYApellido = value;
                var partes = value.Split(',');
                if (partes.Length == 2)
                {
                    Nombre = partes[0].Trim();
                    Apellido = partes[1].Trim();
                }
            }
        }
        public int? Dni { get; set; }
        public TipoSexo? Sexo { get; set; }
        [Required, StringLength(255)]
        public string? LugarDeNacimiento { get; set; }
        public int Edad { get; set; }
        public DateTime? FechaDeNacimiento { get; set; }
        public TipoDamnificado Estado { get; set; }
        public int? FuerzaIntervinienteID { get; set; }
        public string? Destino { get; set; }    
        public int? VehiculoDamnificadoID { get; set; }
        public VehiculoDamnificadoViewModels? VehiculoDamnificado { get; set; }

    }
}
