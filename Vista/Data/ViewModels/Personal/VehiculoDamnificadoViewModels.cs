using System.ComponentModel.DataAnnotations;

namespace Vista.Data.ViewModels.Personal
{
    public class VehiculoDamnificadoViewModel : IEditableViewModel<VehiculoDamnificadoViewModel>
    {
        public int Id { get; set; }

        public int Numero { get; set; }

        [StringLength(255)]
        public string? Marca { get; set; }

        [StringLength(255)]
        public string? Modelo { get; set; }

        public int? Año { get; set; }

        [StringLength(255)]
        public string? Patente { get; set; }

        [StringLength(255)]
        public string? Tipo { get; set; }

        [StringLength(255)]
        public string? Color { get; set; }

        public bool Airbag { get; set; }

        public string? Observaciones { get; set; }

        public bool SeConoceConductor { get; set; }

        [StringLength(500)]
        public string? Dueño { get; set; }

        [StringLength(255)]
        public string? CompañiaAseguradora { get; set; }

        [StringLength(255)]
        public string? NumeroDePoliza { get; set; }

        public DateTime? FechaDeVencimiento { get; set; }

        public int? ConductorId { get; set; }

        public DamnificadoViewModel? Conductor { get; set; }

        public List<DamnificadoViewModel> Pasajeros { get; set; } = new();

        // ✅ Implementación de ICloneable<VehiculoDamnificadoViewModel>
        public VehiculoDamnificadoViewModel Clonar()
        {
            return new VehiculoDamnificadoViewModel
            {
                Id = this.Id,
                Numero = this.Numero,
                Marca = this.Marca,
                Modelo = this.Modelo,
                Año = this.Año,
                Patente = this.Patente,
                Tipo = this.Tipo,
                Color = this.Color,
                Airbag = this.Airbag,
                Observaciones = this.Observaciones,
                SeConoceConductor = this.SeConoceConductor,
                Dueño = this.Dueño,
                CompañiaAseguradora = this.CompañiaAseguradora,
                NumeroDePoliza = this.NumeroDePoliza,
                FechaDeVencimiento = this.FechaDeVencimiento,
                ConductorId = this.ConductorId,
                Conductor = this.Conductor,
                Pasajeros = this.Pasajeros.ToList() // Copia superficial de la lista
            };
        }

        // ✅ Implementación de IUpdatable<VehiculoDamnificadoViewModel>
        public void ActualizarDesde(VehiculoDamnificadoViewModel source)
        {
            this.Id = source.Id;
            this.Numero = source.Numero;
            this.Marca = source.Marca;
            this.Modelo = source.Modelo;
            this.Año = source.Año;
            this.Patente = source.Patente;
            this.Tipo = source.Tipo;
            this.Color = source.Color;
            this.Airbag = source.Airbag;
            this.Observaciones = source.Observaciones;
            this.SeConoceConductor = source.SeConoceConductor;
            this.Dueño = source.Dueño;
            this.CompañiaAseguradora = source.CompañiaAseguradora;
            this.NumeroDePoliza = source.NumeroDePoliza;
            this.FechaDeVencimiento = source.FechaDeVencimiento;
            this.ConductorId = source.ConductorId;
            this.Conductor = source.Conductor;
            this.Pasajeros = source.Pasajeros.ToList();
        }
    }
}