using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using Microsoft.AspNetCore.Http;
using Microsoft.Graph;
using Microsoft.Identity.Client;
using Microsoft.Identity.Web;
using System.Net.Http.Headers;
using System.Security.Claims;
using Vista.Data.ViewModels.Personal;
using Vista.DTOs;

namespace Vista.Services
{
    public interface IEntraIDService
    {
        Task<(BomberoViweModel? bombero, ImagenResultado? foto)> BuscarPorUPNAsync(string upn, CancellationToken token);
        Task<bool> CheckDisponibilidadAsync();
        Task<User> GetUserAsync();

    }

    public class EntraIDService : IEntraIDService
    {
        private readonly GraphServiceClient _graphClient;
        private readonly MicrosoftIdentityConsentAndConditionalAccessHandler _consentHandler;
        private readonly IWebHostEnvironment _env;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITokenAcquisition _tokenAcquisition;

        public EntraIDService(GraphServiceClient graphClient, MicrosoftIdentityConsentAndConditionalAccessHandler consentHandler, IWebHostEnvironment env, IHttpContextAccessor httpContextAccessor, ITokenAcquisition tokenAcquisition)
        {
            _graphClient = graphClient;
            _consentHandler = consentHandler;
            _env = env;
            _httpContextAccessor = httpContextAccessor;
            _tokenAcquisition = tokenAcquisition;
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
            if (_env.IsProduction())
                return false;

            var userPrincipal = _httpContextAccessor.HttpContext?.User;

            if (userPrincipal == null || !userPrincipal.Identity?.IsAuthenticated == true)
                throw new InvalidOperationException("🔒 No hay usuario autenticado en el contexto actual.");

            try
            {
                var graphUser = await GetUserAsync();

                if (graphUser == null)
                    throw new InvalidOperationException("⚠️ No se pudo obtener el usuario actual desde Graph.");

                var users = await _graphClient.Users.Request().Top(1).GetAsync();
                return users?.Count > 0;
            }
            catch (ServiceException ex)
            {
                throw new InvalidOperationException("🚫 Error al verificar disponibilidad de Microsoft Graph.", ex);
            }
        }

        public async Task<User> GetUserAsync()
        {
            var userPrincipal = _httpContextAccessor.HttpContext?.User;

            if (userPrincipal == null || !userPrincipal.Identity?.IsAuthenticated == true)
                throw new InvalidOperationException("🔒 No hay usuario autenticado en el contexto actual.");

            try
            {
                var token = await _tokenAcquisition.GetAccessTokenForUserAsync(new[] { "User.Read" });

                var authProvider = new DelegateAuthenticationProvider(request =>
                {
                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    return Task.CompletedTask;
                });

                var graphClient = new GraphServiceClient(authProvider);

                return await graphClient.Me.Request().GetAsync();
            }
            catch (MsalUiRequiredException ex)
            {
                throw new InvalidOperationException("🧠 Se requiere interacción del usuario para obtener el token.", ex);
            }
            catch (ServiceException ex)
            {
                throw new InvalidOperationException("📡 Error al comunicarse con Microsoft Graph.", ex);
            }
        }
    }
}
