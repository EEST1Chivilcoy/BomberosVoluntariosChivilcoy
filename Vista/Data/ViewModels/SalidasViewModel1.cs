using Vista.Data.Enums;
using Vista.Data.Models.Personas.Personal;
using Vista.Data.Models.Salidas.Componentes;

namespace Vista.Data.ViewModels
{
    public class SalidasViewModel1 : SalidasViewModels
    {
        public int SalidaId { get; set; }


        public string? Receptor { get; set; }

        public int? ReceptorId { get; set; }


        public Bombero? ReceptorBombero { get; set; }

        public int? SeguroId { get; set; }
        public SeguroVivienda? Seguro { get; set; }
       

        public int EncargadoId { get; set; }
        public Bombero Encargado { get; set; }

        public int QuienLlenoId { get; set; }
        public Bombero QuienLleno { get; set; }
        public TipoSalida Discriminador { get; set; }
    }
}
