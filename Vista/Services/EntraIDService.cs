using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using Microsoft.Graph;
using Microsoft.Identity.Web;
using Vista.Data.ViewModels.Personal;
using Vista.DTOs;

namespace Vista.Services
{
    public interface IEntraIDService
    {
        Task<(BomberoViweModel? bombero, ImagenResultado? foto)> BuscarPorUPNAsync(string upn, CancellationToken token);
        Task<bool> CheckDisponibilidadAsync();
    }

    public class EntraIDService : IEntraIDService
    {
        private readonly GraphServiceClient _graphClient;
        private readonly MicrosoftIdentityConsentAndConditionalAccessHandler _consentHandler;
        private readonly IWebHostEnvironment _env;

        public EntraIDService(GraphServiceClient graphClient, MicrosoftIdentityConsentAndConditionalAccessHandler consentHandler, IWebHostEnvironment env)
        {
            _graphClient = graphClient;
            _consentHandler = consentHandler;
            _env = env;
        }

        public async Task<(BomberoViweModel? bombero, ImagenResultado? foto)> BuscarPorUPNAsync(string upn, CancellationToken token)
        {
            if (string.IsNullOrWhiteSpace(upn))
                throw new ArgumentException("El UPN no puede estar vacío.");

            // Construir UPN completo
            string fullUpn = upn.Contains("@") ? upn : $"{upn}@bomberoschivilcoy.org.ar";

            try
            {
                // Buscar usuario
                var user = await _graphClient.Users[fullUpn]
                    .Request()
                    .Select(u => new
                    {
                        u.Id,
                        u.GivenName,
                        u.Surname,
                        u.UserPrincipalName
                    })
                    .GetAsync(token);

                if (user == null)
                    return (null, null);

                // Intentar obtener foto
                byte[]? fotoBytes = null;
                string? contentType = null;

                try
                {
                    var photoMetadata = await _graphClient.Users[fullUpn]
                        .Photo
                        .Request()
                        .GetAsync(token);

                    if (photoMetadata != null)
                    {
                        var photoStream = await _graphClient.Users[fullUpn]
                            .Photo
                            .Content
                            .Request()
                            .GetAsync(token);

                        using var ms = new MemoryStream();
                        await photoStream.CopyToAsync(ms, token);

                        // Ahora guardamos los bytes crudos, no en Base64
                        fotoBytes = ms.ToArray();

                        contentType = photoMetadata.AdditionalData?.ContainsKey("@odata.mediaContentType") == true
                            ? photoMetadata.AdditionalData["@odata.mediaContentType"]?.ToString()
                            : "image/jpeg";

                        var extension = contentType?.Split('/').LastOrDefault() ?? "jpg";
                    }
                }
                catch (ServiceException photoEx) when (photoEx.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    // No tiene foto, continuar
                }

                // Mapear al ViewModel
                var bomberoView = new BomberoViweModel
                {
                    Nombre = user.GivenName ?? string.Empty,
                    Apellido = user.Surname ?? string.Empty,
                    EntraID = Guid.Parse(user.Id),
                    UPN = upn
                };

                var foto = new ImagenResultado
                {
                    Datos = fotoBytes,
                    Formato = contentType
                };

                return (bomberoView, foto);
            }
            catch (MicrosoftIdentityWebChallengeUserException ex)
            {
                _consentHandler.HandleException(ex);
                throw;
            }
        }

        public async Task<bool> CheckDisponibilidadAsync()
        {
            // Si está en producción, siempre false
            if (_env.IsProduction())
                return false;

            try
            {
                // Ejemplo: consultar si el servicio de usuarios responde
                var users = await _graphClient.Users.Request().Top(1).GetAsync();

                // Si la respuesta trae datos, entonces el servicio está OK
                return users?.Count > 0;
            }
            catch (ServiceException)
            {
                // Si hubo error en la llamada (Graph no disponible, permisos, etc.)
                return false;
            }
        }
    }
}
