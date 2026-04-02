using FireForce.Shared.DTOs.Nominatim;

namespace FireForce.Shared.Services
{
    /// <summary>
    /// Interfaz para el servicio que interactúa con la API de Nominatim.
    /// Implementa las buenas prácticas según la política de uso:
    /// https://operations.osmfoundation.org/policies/nominatim/
    /// </summary>
    public interface INominatimService
    {
        /// <summary>
        /// Valida la conexión a la API de Nominatim.
        /// </summary>
        /// <returns>
        /// <c>true</c> si la conexión es exitosa; de lo contrario, <c>false</c>.
        /// </returns>
        Task<bool> CheckApiConnectionAsync();

        /// <summary>
        /// Obtiene sugerencias de direcciones a partir de una entrada libre.
        /// Las consultas son cacheadas para evitar requests repetidos.
        /// </summary>
        /// <param name="input">
        /// Texto de búsqueda libre. Puede ser:
        /// <list type="bullet">
        /// <item><description>Una dirección escrita (ej. "Av. Ceballos 100, Chivilcoy")</description></item>
        /// <item><description>Coordenadas geográficas (ej. "-34.865753, -60.048978")</description></item>
        /// </list>
        /// </param>
        /// <returns>
        /// Una lista de <see cref="Direccion"/> con las coincidencias encontradas.
        /// Devuelve lista vacía si no hay conexión, ocurre un error, o la entrada está vacía.
        /// </returns>
        Task<List<Direccion>> GetSuggestionsAsync(string input);
    }
}
