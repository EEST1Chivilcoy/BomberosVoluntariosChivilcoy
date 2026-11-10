using FireForce.Shared.Enums.Firmas;

namespace FireForce.Client.Data.ViewModels.Incidentes
{
    /// <summary>
    /// Entrada de la bitácora de un incidente, para mostrar en la línea de tiempo del detalle.
    /// </summary>
    public class MovimientoIncidenteViewModel
    {
        public int Id { get; set; }

        public DateTime Fecha { get; set; }

        public TipoMovimiento Tipo { get; set; }

        public string Descripcion { get; set; } = string.Empty;

        public string AutorNombre { get; set; } = string.Empty;
    }
}
