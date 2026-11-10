namespace FireForce.Shared.Services
{
    /// <summary>
    /// Calcula una marca de versión para archivos estáticos (CSS, JS), para usar como query
    /// string de cache-busting en los <c>&lt;link&gt;</c> que viven dentro de componentes Blazor,
    /// donde el tag helper <c>asp-append-version</c> no está disponible.
    /// </summary>
    public interface IVersionadorEstaticos
    {
        /// <summary>
        /// Devuelve una marca de versión estable para la ruta relativa al wwwroot
        /// (por ejemplo <c>css/institucional.css</c>). Cambia cuando cambia el archivo.
        /// </summary>
        string ObtenerVersion(string rutaRelativa);
    }
}
