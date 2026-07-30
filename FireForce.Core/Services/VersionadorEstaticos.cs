using FireForce.Shared.Services;

namespace FireForce.Core.Services
{
    /// <summary>
    /// Implementación basada en la fecha de modificación del archivo dentro del wwwroot.
    /// Es el equivalente casero de <c>asp-append-version</c> para los links declarados en
    /// componentes Blazor (por ejemplo el de institucional.css en InicioLayout).
    /// </summary>
    public class VersionadorEstaticos : IVersionadorEstaticos
    {
        private readonly IWebHostEnvironment _entorno;

        public VersionadorEstaticos(IWebHostEnvironment entorno)
        {
            _entorno = entorno;
        }

        public string ObtenerVersion(string rutaRelativa)
        {
            var archivo = _entorno.WebRootFileProvider.GetFileInfo(rutaRelativa);

            // Si el archivo no existe, una versión fija evita romper el link.
            return archivo.Exists
                ? archivo.LastModified.UtcTicks.ToString("x")
                : "0";
        }
    }
}
