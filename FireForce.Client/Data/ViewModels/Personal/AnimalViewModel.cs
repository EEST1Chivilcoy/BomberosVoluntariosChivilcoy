using DocumentFormat.OpenXml.Bibliography;
using FireForce.Data.Models.Personas;
using FireForce.Shared.Enums;
using System.ComponentModel.DataAnnotations;

namespace FireForce.Client.Data.ViewModels.Personal
{
    public class AnimalViewModel : IEditableViewModel<AnimalViewModel>
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Por favor, especifique un tipo de animal")]
        public TipoAnimal? Tipo {  get; set; }

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

        [StringLength(255)]
        public string? NombreResponsable { get; set; }

        [StringLength(255)]
        public string? ApellidoResponsable { get; set; }

        [StringLength(255)]
        public string? DniResponsable { get; set; }

        public string NombreYApellidoResponsable
        {
            get
            {
                if (NombreResponsable is null && ApellidoResponsable is null) return "Desconocido";
                else if (ApellidoResponsable is null && NombreResponsable is not null) return NombreResponsable;
                else return $"{ApellidoResponsable}, {NombreResponsable}";
            }
        }

        public void ActualizarDesde(AnimalViewModel source)
        {
            Tipo = source.Tipo;
            TipoOtro = source.TipoOtro;
            Estado = source.Estado;
            Cantidad = source.Cantidad;
            Nombre = source.Nombre;
            Observaciones = source.Observaciones;
            NumeroOrden = source.NumeroOrden;
            SeConoceResponsable = source.SeConoceResponsable;
            NombreResponsable = source.NombreResponsable;
            ApellidoResponsable = source.ApellidoResponsable;
            DniResponsable = source.DniResponsable;
        }

        public AnimalViewModel Clonar()
        {
            return new AnimalViewModel
            {
                Tipo = this.Tipo,
                TipoOtro = this.TipoOtro,
                Estado = this.Estado,
                Cantidad = this.Cantidad,
                Nombre = this.Nombre,
                Observaciones = this.Observaciones,
                NumeroOrden = this.NumeroOrden,
                SeConoceResponsable = this.SeConoceResponsable,
                NombreResponsable = this.NombreResponsable,
                ApellidoResponsable = this.ApellidoResponsable,
                DniResponsable = this.DniResponsable
            };
        }
    }
}
