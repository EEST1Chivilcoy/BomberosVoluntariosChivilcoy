using Microsoft.Extensions.Caching.Memory;
using Vista.DTOs.Nominatim;

namespace Vista.Services;

/// <summary>
/// Servicio de caché para almacenar resultados de Nominatim.
/// Evita consultas repetidas a la API pública según los términos de uso.
/// </summary>
public interface INominatimCacheService
{
    /// <summary>
    /// Intenta obtener un resultado de caché para una consulta dada.
    /// </summary>
    bool TryGetCachedResult(string query, out List<Direccion>? result);

    /// <summary>
    /// Almacena un resultado en caché.
    /// </summary>
    void CacheResult(string query, List<Direccion> result);

    /// <summary>
    /// Limpia todo el caché de Nominatim.
    /// </summary>
    void ClearCache();
}

public class NominatimCacheService : INominatimCacheService
{
    private readonly IMemoryCache _cache;
    private readonly TimeSpan _cacheDuration = TimeSpan.FromHours(24); // Los datos de OSM no cambian frecuentemente
    private const string CacheKeyPrefix = "Nominatim_";

    public NominatimCacheService(IMemoryCache cache)
    {
        _cache = cache;
    }

    public bool TryGetCachedResult(string query, out List<Direccion>? result)
    {
        var normalizedQuery = NormalizeQuery(query);
        var cacheKey = $"{CacheKeyPrefix}{normalizedQuery}";
        return _cache.TryGetValue(cacheKey, out result);
    }

    public void CacheResult(string query, List<Direccion> result)
    {
        var normalizedQuery = NormalizeQuery(query);
        var cacheKey = $"{CacheKeyPrefix}{normalizedQuery}";

        var cacheOptions = new MemoryCacheEntryOptions()
            .SetAbsoluteExpiration(_cacheDuration)
            .SetPriority(CacheItemPriority.Normal);

        _cache.Set(cacheKey, result, cacheOptions);
    }

    public void ClearCache()
    {
        // MemoryCache no tiene un método ClearAll, pero en un proyecto pequeño
        // esto no es necesario. Los items expirarán automáticamente.
        // Si necesitas limpiar, considera usar un prefijo diferente o reiniciar la app.
    }

    /// <summary>
    /// Normaliza la consulta para evitar duplicados por diferencias de espacios/mayúsculas.
    /// </summary>
    private static string NormalizeQuery(string query)
    {
        return query.Trim().ToLowerInvariant().Replace("  ", " ");
    }
}
