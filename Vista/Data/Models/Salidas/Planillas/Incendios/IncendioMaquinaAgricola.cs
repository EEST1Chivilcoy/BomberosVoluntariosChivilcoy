using Vista.Data.Models.Salidas.Componentes;

namespace Vista.Data.Models.Salidas.Planillas.Incendios
{
    public class IncendioMaquinaAgricola : Incendio
    {
        public VehiculoAfectado? VehiculoAfectado { get; set; }

        // TODO: Hacer igual que vehiculos (múltiples) y agregar tipos específicos
        // de equipos rurales
    }
}
