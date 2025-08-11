using System.ComponentModel.DataAnnotations;
using Vista.Data.Enums;
using Vista.Data.Models.Grupos.Brigadas;

namespace Vista.Data.ViewModels.Personal
{
    public class BomberoViweModel
    {
        public int Id { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public DateTime? FechaAceptacion { get; set; }
        [Required, StringLength(255)]
        public string Nombre { get; set; }
        [Required, StringLength(255)]
        public string Apellido { get; set; }
        public string NombreYApellido
        {
            get { return Nombre + "," + Apellido; }
        }
        public string ApellidoYNombre
        {
            get { return Apellido + "," + Nombre; }
        }
        public EstadoBombero Estado { get; set; }
        [Required]
        public int NumeroLegajo { get; set; }
        public EscalafonJerarquico Grado { get; set; }
        public TipoDotaciones Dotacion { get; set; }
        public TipoGrupoSanguineo GrupoSanguineo { get; set; }
        public int? Altura { get; set; }
        public int? Peso { get; set; }
        public Brigada? Brigada { get; set; }
        public bool EsChofer { get; set; }
        public DateTime? VencimientoRegistro { get; set; }
        public TipoSexo Sexo { get; set; }
        [StringLength(255)]
        public string? Direccion { get; set; }
        [StringLength(255)]
        public string? LugarNacimiento { get; set; }
        [Required]
        public int Documento { get; set; }
        [StringLength(255)]
        public string? Observaciones { get; set; }
        [StringLength(255)]
        public string? Profesion { get; set; }
        [StringLength(255)]
        public string? NivelEstudios { get; set; }
        [StringLength(255)]
        public string? NumeroIoma { get; set; }
        //Contacto-------------------------------
        [StringLength(255)]
        public string? TelefonoCel { get; set; }
        [StringLength(255)]
        public string? TelefonoLaboral { get; set; }
        [StringLength(255)]
        public string? TelefonoFijo { get; set; }
        [StringLength(255)]
        public string? Email { get; set; }
        //Imagen-------------------------------
        public string? UrlImagen { get; set; }
        //Brigada-------------------------------
        public int BrigadaId { get; set; }
        public string NombreBrigada { get; set; }

        public bool Selected { get; set; }
    }
}
