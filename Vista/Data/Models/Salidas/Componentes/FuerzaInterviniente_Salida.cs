using System.ComponentModel.DataAnnotations.Schema;
using Vista.Data.Models.Grupos.FuerzasIntervinientes;
using Vista.Data.Models.Personas;
using Vista.Data.Models.Salidas.Planillas;

namespace Vista.Data.Models.Salidas.Componentes
{
    /// <summary>
    /// Clase que representa la fuerza interviniente en una salida.
    /// </summary>
    public class FuerzaInterviniente_Salida
    {
        /// <summary>
        /// Identificador único de la fuerza interviniente en una salida.
        /// </summary>
        public int Id { get; set; }

        // Datos específicos de la fuerza interviniente en la salida

        /// <summary>
        /// Numero de unidad del vehiculo de la fuerza interviniente que participó en la salida.
        /// </summary>
        public string NumeroUnidad { get; set; } = null!;

        /// <summary>
        /// Nombre y apellido del encargado de la fuerza interviniente que participó en la salida.
        /// </summary>
        public string EncargadoApellidoyNombre { get; set; } = null!;

        // Relación con la Fuerza Interviniente

        /// <summary>
        /// Identificador de la fuerza interviniente asociada a la salida.
        /// </summary>
        public int FuerzaIntervinienteId { get; set; }

        /// <summary>
        /// Fuerza interviniente asociada a la salida.
        /// </summary>
        [ForeignKey(nameof(FuerzaIntervinienteId))]
        public FuerzaInterviniente Fuerzainterviniente { get; set; } = null!;

        // Relacion con Damnificado

        public List<Damnificado_Salida> Damnificados { get; set; } = new();

        // Relación con la Salida

        /// <summary>
        /// Identificador de la salida asociada.
        /// </summary>
        public int SalidaId { get; set; }

        /// <summary>
        /// Salida asociada a la fuerza interviniente.
        /// </summary>
        [ForeignKey(nameof(SalidaId))]
        public Salida Salida { get; set; } = null!;
    }
}
