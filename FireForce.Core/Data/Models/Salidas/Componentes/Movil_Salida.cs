using Vista.Data.Models.Salidas.Planillas;
using System.ComponentModel.DataAnnotations.Schema;
using Vista.Data.Enums;
using Vista.Data.Models.Personas.Personal;
using Vista.Data.Models.Vehiculos.Flota;

namespace Vista.Data.Models.Salidas.Componentes
{
    public class Movil_Salida
    {
        /// <summary>
        /// Identificador único de la salida del vehículo móvil.
        /// </summary>
        public int Movil_SalidaId { get; set; }

        /// <summary>
        /// Indica si se cargó combustible en la salida.
        /// </summary>
        public bool CargoCombustible { get; set; }

        /// <summary>
        /// Número de factura correspondiente a la carga de combustible. Este campo es opcional y solo se llena si se cargó combustible.
        /// </summary>
        public string? NumeroFactura { get; set; }

        /// <summary>
        /// Fecha de la factura por la carga de combustible. Este campo es opcional y se utiliza solo si se cargó combustible.
        /// </summary>
        public DateTime? FechaFactura { get; set; }

        /// <summary>
        /// Tipo de combustible cargado en el vehículo móvil (por ejemplo, gasolina, diésel). Este campo es opcional.
        /// </summary>
        public string? TipoConbustible { get; set; }

        /// <summary>
        /// Cantidad de litros de combustible cargados en el vehículo móvil. Este campo es opcional.
        /// </summary>
        public string? CantidadLitros { get; set; }

        /// <summary>
        /// Nombre completo de la persona que llenó el vehículo de combustible. Este campo es opcional.
        /// </summary>
        public string? QuienLleno { get; set; }

        /// <summary>
        /// Teléfono de la persona que llenó el vehículo de combustible. Este campo es opcional.
        /// </summary>
        public string? TelefonoQuienLleno { get; set; }

        public DotacionesSalidas DotacionSalida { get; set; }

        /// <summary>
        /// Identificador de la salida registrada, relacionada con la clase `Salida`.
        /// </summary>        
        public int SalidaId { get; set; }

        /// <summary>
        /// Relación con la salida asociada. Utiliza el identificador `SalidaId` como clave foránea.
        /// </summary>
        [ForeignKey(nameof(SalidaId))]
        public Salida Salida { get; set; } = null!;

        /// <summary>
        /// Identificador del bombero (chofer) que condujo el vehículo durante la salida.
        /// </summary>
        public int PersonaId { get; set; }

        /// <summary>
        /// Relación con el bombero (chofer) que condujo el vehículo. Utiliza el identificador `PersonaId` como clave foránea.
        /// </summary>
        [ForeignKey(nameof(PersonaId))]
        public Bombero Chofer { get; set; } = null!;

        /// <summary>
        /// Identificador del vehículo móvil asociado a esta salida.
        /// </summary>
        public int MovilId { get; set; }

        /// <summary>
        /// Relación con el vehículo móvil que participó en la salida.
        /// </summary>

        [ForeignKey(nameof(MovilId))]
        public Movil Movil { get; set; } = null!;

        /// <summary>
        /// Kilometraje registrado al momento de la llegada del móvil al cuartel.
        /// </summary>
        public int KmLlegada { get; set; }
    }
}
