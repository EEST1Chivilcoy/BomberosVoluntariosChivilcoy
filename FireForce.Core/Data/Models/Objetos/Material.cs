using System.ComponentModel.DataAnnotations;
using Vista.Data.Enums;

namespace Vista.Data.Models.Objetos
{
    public class Material
    {
        public int MaterialId { get; set; }

        public DateTime FechaAlta { get; set; }
        [Required, StringLength(255)]
        public string Descripcion { get; set; }
        [StringLength(255)]
        public string? Codigo { get; set; }
        public int Stock { get; set; }

        public TipoRubro Rubro { get; set; }
    }
}
