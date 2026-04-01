using Vista.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace Vista.Data.Models.Salidas.Planillas
{
    public class MaterialPeligroso : Salida
    {
        //Localización, datos del solicitante, personas damnificadas y datos del seguro
        public CategoriaMaterialPeligroso Tipo { get; set; }  
        [Required, StringLength(255)]  
        public string Sustancias { get; set; } // TODO: Consultar a Agustín

        //Acciones sobre los materiales
        public bool Controlada { get; set; }
        public bool Venteo { get; set; }
        public bool DilucionDeVapores { get; set; }
        public bool Neutralizacion { get; set; }
        public bool Trasvase { get; set; }
        public bool OtraAccionesMateriales { get; set; }
        [Required, StringLength(255)]
        public string DetallesAccionesMateriales { get; set; }

        //Acciones sobre las personas
        public TipoEvacuacion Evacuacion { get; set; }
        public bool Descontaminacion { get; set; }
        public bool Confinamiento { get; set; }
        public bool SinAccion { get; set; }
        public bool OtraAccionesPersonas { get; set; }
        [Required, StringLength(255)]
        public string DetallesAccionesPersonas { get; set; }

        //Superficie Aferctada
        public TipoSuperficie TipoSuperficie { get; set; }
        public int CantidadAfectadaMaterialPeligroso { get; set; }

        //Situación
        public TipoSituacionExplosion TipoSituacion { get; set; }
    }
}
