namespace Vista.Services;

/// <summary>
/// Configuración para el servicio de Nominatim.
/// Externalizar la configuración permite cambiar de proveedor sin modificar código,
/// cumpliendo con el requisito de la política de uso de Nominatim.
/// </summary>
public class NominatimSettings
{
    public const string SectionName = "Nominatim";

    /// <summary>
    /// URL base de la API de Nominatim.
    /// Por defecto usa el servidor público de OSM.
    /// Puede cambiarse a un proveedor alternativo o instancia propia.
    /// </summary>
    public string BaseUrl { get; set; } = "https://nominatim.openstreetmap.org";

    /// <summary>
    /// Nombre de la aplicación para el User-Agent.
    /// REQUERIDO por la política de Nominatim.
    /// </summary>
    public string AppName { get; set; } = "BomberosVoluntariosChivilcoy";

    /// <summary>
    /// Versión de la aplicación para el User-Agent.
    /// </summary>
    public string AppVersion { get; set; } = "1.0";

    /// <summary>
    /// Email de contacto para el User-Agent.
    /// RECOMENDADO por la política de Nominatim para que puedan contactarte
    /// si hay problemas con el uso de la API.
    /// </summary>
    public string ContactEmail { get; set; } = "contacto@bomberoschivilcoy.org.ar";

    /// <summary>
    /// Intervalo mínimo entre requests en milisegundos.
    /// La política de Nominatim requiere máximo 1 request por segundo (1000ms).
    /// Usamos 1100ms para tener margen de seguridad.
    /// </summary>
    public int MinRequestIntervalMs { get; set; } = 1100;

    /// <summary>
    /// Límite de resultados por consulta.
    /// Mantener bajo para reducir carga en el servidor.
    /// </summary>
    public int ResultLimit { get; set; } = 10;

    /// <summary>
    /// Código de país para filtrar resultados (ISO 3166-1 alpha-2).
    /// </summary>
    public string CountryCode { get; set; } = "ar";

    /// <summary>
    /// Genera el User-Agent según los requisitos de Nominatim.
    /// Formato: AppName/Version (ContactEmail)
    /// </summary>
    public string GetUserAgent()
    {
        return $"{AppName}/{AppVersion} ({ContactEmail})";
    }
}
