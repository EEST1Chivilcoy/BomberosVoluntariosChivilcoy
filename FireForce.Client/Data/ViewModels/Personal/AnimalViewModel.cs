using FireForce.Data.Models.Personas;
using FireForce.Shared.Enums;
using System.ComponentModel.DataAnnotations;

namespace FireForce.Client.Data.ViewModels.Personal
{
    class AnimalViewModel
    {
        [Required(ErrorMessage = "Por favor, especifique un tipo de animal")]
        public TipoAnimal Tipo {  get; set; }

        public string? TipoOtro { get; set; }

        [Required(ErrorMessage = "Debe indicar en qué estado se encuentra el animal")]
        public EstadoAnimal Estado { get; set; }

        public int Cantidad { get; set; }

        [StringLength(255)]
        public string? Nombre { get; set; }

        [StringLength(255)]
        public string? Observaciones { get; set; }

        public int NumeroOrden { get; set; }

        public bool SeConoceResponsable { get; set; }

        public int? DamnificadoId { get; set; }

        public Damnificado_Salida? Damnificado { get; set; }

        public DamnificadoViewModel? DamnificadoViewModel { get; set; }

        public string NombreDamnificado
        {
            get
            {
                if (DamnificadoViewModel is null) return "Desconocido";
                else return DamnificadoViewModel.NombreYApellido;
            }
        }
    }
}
