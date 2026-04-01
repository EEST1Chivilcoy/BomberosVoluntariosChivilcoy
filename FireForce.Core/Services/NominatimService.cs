using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.JSInterop;
using System.Net.Http.Json;
using Vista.Data.Mappers;
using Vista.DTOs.Nominatim;
using Vista.DTOs.Nominatim.Raw;

namespace Vista.Services;

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

/// <summary>
/// Servicio para interactuar con la API de Nominatim (OpenStreetMap).
/// 
/// Cumple con la política de uso oficial:
/// - Rate limiting: máximo 1 request por segundo
/// - User-Agent identificativo con email de contacto
/// - Caché de resultados para evitar consultas repetidas
/// - Sin reintentos agresivos ante errores
/// - Configuración externalizada para cambiar de proveedor
/// </summary>
public class NominatimService : INominatimService
{
    private readonly HttpClient _httpClient;
    private readonly IJSRuntime _jsRuntime;
    private readonly INominatimCacheService _cacheService;
    private readonly NominatimSettings _settings;
    private readonly ILogger<NominatimService> _logger;

    // Rate limiting: garantiza máximo 1 request por segundo (política de Nominatim)
    private static readonly SemaphoreSlim _rateLimitSemaphore = new(1, 1);
    private static DateTime _lastRequestTime = DateTime.MinValue;

    public NominatimService(
        HttpClient httpClient,
        IJSRuntime jsRuntime,
        INominatimCacheService cacheService,
        IOptions<NominatimSettings> settings,
        ILogger<NominatimService> logger)
    {
        _httpClient = httpClient;
        _jsRuntime = jsRuntime;
        _cacheService = cacheService;
        _settings = settings.Value;
        _logger = logger;

        ConfigureHttpClient();
    }

    /// <summary>
    /// Configura el HttpClient con User-Agent según los requisitos de Nominatim.
    /// El User-Agent debe identificar la aplicación (no genérico como "Mozilla/5.0").
    /// </summary>
    private void ConfigureHttpClient()
    {
        _httpClient.DefaultRequestHeaders.Clear();
        _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd(_settings.GetUserAgent());

        // Agregar header de referencia si es webapp
        if (!string.IsNullOrEmpty(_settings.ContactEmail))
        {
            _httpClient.DefaultRequestHeaders.Add("From", _settings.ContactEmail);
        }

        _httpClient.BaseAddress = new Uri(_settings.BaseUrl);
        _httpClient.Timeout = TimeSpan.FromSeconds(30);
    }

    public async Task<bool> CheckApiConnectionAsync()
    {
        try
        {
            // Aplicar rate limiting antes de la consulta
            await ApplyRateLimitingAsync();

            var url = $"{_settings.BaseUrl}/search?city=Chivilcoy&country=Argentina&format=json&limit=1";
            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                _logger.LogInformation("Conexión exitosa a Nominatim API");
                return true;
            }

            // Manejar diferentes códigos de error
            var errorMessage = response.StatusCode switch
            {
                System.Net.HttpStatusCode.TooManyRequests =>
                    "⚠️ Rate limit excedido. Esperá un momento antes de intentar nuevamente.",
                System.Net.HttpStatusCode.Forbidden =>
                    "🚫 Acceso bloqueado. Verificá el User-Agent y la política de uso.",
                System.Net.HttpStatusCode.ServiceUnavailable =>
                    "🔧 Servicio temporalmente no disponible. Intentá más tarde.",
                _ =>
                    $"❌ Error de conexión: {response.StatusCode}"
            };

            await LogErrorToConsoleAsync(errorMessage);
            _logger.LogWarning("Nominatim API: {Error}", errorMessage);
            return false;
        }
        catch (TaskCanceledException)
        {
            await LogErrorToConsoleAsync("⏱️ Timeout al conectar con Nominatim. La API puede estar sobrecargada.");
            return false;
        }
        catch (HttpRequestException ex)
        {
            await LogErrorToConsoleAsync($"❌ Error de red: {ex.Message}");
            _logger.LogError(ex, "Error de conexión a Nominatim");
            return false;
        }
        catch (Exception ex)
        {
            await LogErrorToConsoleAsync($"❌ Error inesperado: {ex.Message}");
            _logger.LogError(ex, "Error inesperado al verificar conexión a Nominatim");
            return false;
        }
    }

    public async Task<List<Direccion>> GetSuggestionsAsync(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return [];

        var normalizedInput = input.Trim();

        // Verificar caché primero (evita consultas repetidas según política)
        if (_cacheService.TryGetCachedResult(normalizedInput, out var cachedResult) && cachedResult is not null)
        {
            _logger.LogDebug("Resultado obtenido de caché para: {Query}", normalizedInput);
            return cachedResult;
        }

        try
        {
            // Aplicar rate limiting antes de la consulta
            await ApplyRateLimitingAsync();

            var query = Uri.EscapeDataString(normalizedInput);
            var url = $"/search?format=json&q={query}&addressdetails=1&limit={_settings.ResultLimit}&countrycodes={_settings.CountryCode}";

            var rawResults = await _httpClient.GetFromJsonAsync<List<NominatimRaw>>(url);

            var result = rawResults is null
                ? []
                : NominatimMapper.Map(rawResults).Direcciones;

            // Cachear resultado para futuras consultas
            _cacheService.CacheResult(normalizedInput, result);
            _logger.LogDebug("Resultado cacheado para: {Query} ({Count} direcciones)", normalizedInput, result.Count);

            return result;
        }
        catch (HttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
        {
            // Rate limit excedido - NO reintentar agresivamente
            await LogErrorToConsoleAsync("⚠️ Demasiadas solicitudes. Por favor, esperá un momento.");
            _logger.LogWarning("Rate limit excedido en Nominatim para query: {Query}", normalizedInput);
            return [];
        }
        catch (TaskCanceledException)
        {
            await LogErrorToConsoleAsync("⏱️ La búsqueda tardó demasiado. Intentá con una dirección más específica.");
            return [];
        }
        catch (HttpRequestException ex)
        {
            await LogErrorToConsoleAsync($"❌ Error de conexión al buscar direcciones: {ex.Message}");
            _logger.LogError(ex, "Error HTTP al buscar en Nominatim: {Query}", normalizedInput);
            return [];
        }
        catch (Exception ex)
        {
            await LogErrorToConsoleAsync($"❌ Error al buscar direcciones: {ex.Message}");
            _logger.LogError(ex, "Error al buscar en Nominatim: {Query}", normalizedInput);
            return [];
        }
    }

    /// <summary>
    /// Aplica rate limiting para cumplir con el máximo de 1 request por segundo.
    /// Usa un semáforo para garantizar que las consultas sean secuenciales.
    /// </summary>
    private async Task ApplyRateLimitingAsync()
    {
        await _rateLimitSemaphore.WaitAsync();
        try
        {
            var timeSinceLastRequest = DateTime.UtcNow - _lastRequestTime;
            var requiredDelay = TimeSpan.FromMilliseconds(_settings.MinRequestIntervalMs);

            if (timeSinceLastRequest < requiredDelay)
            {
                var delayTime = requiredDelay - timeSinceLastRequest;
                _logger.LogDebug("Rate limiting: esperando {Delay}ms antes del siguiente request", delayTime.TotalMilliseconds);
                await Task.Delay(delayTime);
            }

            _lastRequestTime = DateTime.UtcNow;
        }
        finally
        {
            _rateLimitSemaphore.Release();
        }
    }

    private async Task LogErrorToConsoleAsync(string message)
    {
        try
        {
            await _jsRuntime.InvokeVoidAsync("console.error", message);
        }
        catch
        {
            // Si JSRuntime no está disponible (ej. prerendering), ignorar
        }
    }
}
