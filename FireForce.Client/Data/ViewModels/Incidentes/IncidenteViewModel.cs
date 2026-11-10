using FireForce.Shared.Enums.Discriminadores;
using FireForce.Shared.Enums.Firmas;

namespace FireForce.Client.Data.ViewModels.Incidentes
{
    /// <summary>
    /// Proyección de un incidente para la grilla y el detalle. Es de solo lectura: se arma con
    /// <c>IncidenteMapper</c> y no vuelve al servicio.
    /// </summary>
    public class IncidenteViewModel
    {
        public int Id { get; set; }

        public DateTime Fecha { get; set; }

        public string Descripcion { get; set; } = string.Empty;

        public CategoriaIncidente Categoria { get; set; }

        public EstadoIncidente Estado { get; set; }

        public TipoDeIncidente Tipo { get; set; }

        /// <summary>Móvil o dependencia sobre la que se reportó, ya resuelto a texto.</summary>
        public string ObjetoAfectado { get; set; } = string.Empty;

        public int SolicitanteId { get; set; }

        public string SolicitanteNombre { get; set; } = string.Empty;

        /// <summary>Un no bombero solo puede reportar novedades y nunca recibe firmas.</summary>
        public bool SolicitanteEsBombero { get; set; }

        /// <summary>
        /// Encargado del incidente. Mientras la autenticación esté deshabilitada, es también quien
        /// se registra como autor de las acciones (aprobar, rechazar, derivar).
        /// </summary>
        public int EncargadoId { get; set; }

        public string EncargadoNombre { get; set; } = string.Empty;

        public int CantidadFirmas { get; set; }

        public int CantidadTareas { get; set; }

        public List<TareaViewModel> Tareas { get; set; } = new();

        public List<MovimientoIncidenteViewModel> Movimientos { get; set; } = new();

        /// <summary>Un incidente ya resuelto no admite aprobar, rechazar ni derivar.</summary>
        public bool EstaResuelto =>
            Estado is EstadoIncidente.Aprobado or EstadoIncidente.Desaprobado;

        /// <summary>Las novedades no otorgan firmas, y un no bombero tampoco puede recibirlas.</summary>
        public bool PuedeRecibirFirmas =>
            Categoria != CategoriaIncidente.Novedad && SolicitanteEsBombero;
    }
}
