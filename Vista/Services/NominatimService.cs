using Microsoft.JSInterop;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Vista.Data.Mappers;
using Vista.DTOs.Nominatim;
using Vista.DTOs.Nominatim.Raw;

namespace Vista.Services
{
    /// <summary>
    /// Interfaz para el servicio que interactúa con la API de Nominatim.
    /// </summary>
    public interface INominatimService
    {
        /// <summary>
        /// Valida la conexión a la API de Georef.
        /// </summary>
        /// <returns>
        /// Un <see cref="Task{TResult}"/> que representa la operación asíncrona. El valor resultante es <c>true</c> si la conexión es exitosa; de lo contrario, <c>false</c>.
        /// </returns>
        Task<bool> CheckApiConnectionAsync();

        /// <summary>
        /// Obtiene sugerencias de direcciones a partir de una entrada libre, que puede ser un nombre de calle, una dirección parcial o coordenadas geográficas.
        /// </summary>
        /// <param name="input">
        /// Texto de búsqueda libre. Puede ser:
        /// <list type="bullet">
        /// <item><description>Una dirección escrita (ej. "Av. Ceballos 100, Chivilcoy")</description></item>
        /// <item><description>Coordenadas geográficas (ej. "-34.865753, -60.048978")</description></item>
        /// </list>
        /// </param>
        /// <returns>
        /// Una lista de objetos <see cref="Direccion"/> con las coincidencias encontradas. Si no hay conexión o ocurre un error durante la búsqueda, se devuelve una lista vacía.
        /// </returns>
        Task<List<Direccion>> GetSuggestionsAsync(string input);
    }

    public class NominatimService : INominatimService
    {
        private readonly HttpClient _httpClient;
        private readonly IJSRuntime _jsRuntime;

        public NominatimService(HttpClient httpClient, IJSRuntime jsRuntime)
        {
            _httpClient = httpClient;
            _jsRuntime = jsRuntime;

            _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (compatible; AcmeInc/1.0)");
        }

        public async Task<bool> CheckApiConnectionAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("https://nominatim.openstreetmap.org/search?city=Chivilcoy&country=Argentina&format=json&limit=1");

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    var errorMsg = $"API connection failed with status code: {response.StatusCode}";
                    await LogErrorToConsoleAsync(errorMsg);
                    return false;
                }
            }
            catch (Exception ex)
            {
                await LogErrorToConsoleAsync($"Error checking API connection: {ex.Message}");
                return false;
            }
        }

        public async Task<List<Direccion>> GetSuggestionsAsync(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return new List<Direccion>();

            var query = Uri.EscapeDataString(input.Trim());
            var url = $"https://nominatim.openstreetmap.org/search?format=json&q={query}&addressdetails=1&limit=10&countrycodes=ar";

            try
            {
                var rawResults = await _httpClient.GetFromJsonAsync<List<NominatimRaw>>(url);
                return rawResults is null
                    ? new List<Direccion>()
                    : NominatimMapper.Map(rawResults).Direcciones;
            }
            catch (HttpRequestException httpEx)
            {
                await LogErrorToConsoleAsync($"❌ Error de conexión al buscar direcciones: {httpEx.Message}");
                return new List<Direccion>();
            }
            catch (Exception ex)
            {
                await LogErrorToConsoleAsync($"❌ Error al buscar direcciones: {ex.Message}");
                return new List<Direccion>();
            }
        }

        private async Task LogErrorToConsoleAsync(string message)
        {
            await _jsRuntime.InvokeVoidAsync("console.error", message);
        }
    }
}
