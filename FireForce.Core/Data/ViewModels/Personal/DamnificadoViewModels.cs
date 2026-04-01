using System.ComponentModel.DataAnnotations;
using Vista.Data.Enums;

namespace Vista.Data.ViewModels.Personal
{
    public class DamnificadoViewModel : IEditableViewModel<DamnificadoViewModel>
    {
        public int Id { get; set; }

        public int Numero { get; set; }

        [StringLength(255)]
        public string? Nombre { get; set; }

        [StringLength(255)]
        public string? Apellido { get; set; }

        public string? Dni { get; set; }

        public TipoSexo? Sexo { get; set; }

        [StringLength(255)]
        public string? LugarDeNacimiento { get; set; }

        public int? Edad { get; set; }

        public DateTime? FechaDeNacimiento { get; set; }

        public TipoDamnificado? Estado { get; set; }

        public int? FuerzaIntervinienteId { get; set; }

        public FuerzaIntervinienteViewModel? FuerzaInterviniente { get; set; }

        public string? Destino { get; set; }

        public string NombreYApellido
        {
            get
            {
                var partes = new[] { Apellido?.Trim(), Nombre?.Trim() }
                    .Where(p => !string.IsNullOrEmpty(p));

                var nombreCompleto = partes.Any() ? string.Join(", ", partes) : "Sin nombre";

                return $"#{Numero} - {nombreCompleto}";
            }
        }

        public string FuerzaIntervinienteNombre
        {
            get
            {
                if (FuerzaInterviniente != null)
                    return $"{FuerzaInterviniente.NombreCompleto}";
                return "Sin asignar";
            }
        }

        // ✅ Implementación de ICloneable<DamnificadoViewModel>
        public DamnificadoViewModel Clonar()
        {
            return new DamnificadoViewModel
            {
                Numero = this.Numero,
                Nombre = this.Nombre,
                Apellido = this.Apellido,
                Dni = this.Dni,
                Sexo = this.Sexo,
                LugarDeNacimiento = this.LugarDeNacimiento,
                Edad = this.Edad,
                FechaDeNacimiento = this.FechaDeNacimiento,
                Estado = this.Estado,
                FuerzaIntervinienteId = this.FuerzaIntervinienteId,
                FuerzaInterviniente = this.FuerzaInterviniente,
                Destino = this.Destino
            };
        }

        // ✅ Implementación de IUpdatable<DamnificadoViewModel>
        public void ActualizarDesde(DamnificadoViewModel source)
        {
            this.Numero = source.Numero;
            this.Nombre = source.Nombre;
            this.Apellido = source.Apellido;
            this.Dni = source.Dni;
            this.Sexo = source.Sexo;
            this.LugarDeNacimiento = source.LugarDeNacimiento;
            this.Edad = source.Edad;
            this.FechaDeNacimiento = source.FechaDeNacimiento;
            this.Estado = source.Estado;
            this.FuerzaIntervinienteId = source.FuerzaIntervinienteId;
            this.FuerzaInterviniente = source.FuerzaInterviniente;
            this.Destino = source.Destino;
        }
    }
}