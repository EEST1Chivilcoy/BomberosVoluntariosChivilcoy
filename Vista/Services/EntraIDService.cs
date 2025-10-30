using Microsoft.Graph;
using Microsoft.Identity.Client;
using Microsoft.Identity.Web;
using System.Net.Http.Headers;
using Vista.Data.ViewModels.Personal;
using Vista.DTOs;

namespace Vista.Services
{
    public interface IEntraIDService
    {
        Task<(BomberoViweModel? bombero, ImagenResultado? foto)> BuscarPorUPNAsync(string upn, CancellationToken token);
        Task<bool> CheckDisponibilidadAsync();
        Task<User?> GetUserAsync();
    }

    public class EntraIDService : IEntraIDService
    {
        private readonly GraphServiceClient _graphClient;
        private readonly MicrosoftIdentityConsentAndConditionalAccessHandler _consentHandler;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITokenAcquisition _tokenAcquisition;
        private readonly ILogger<EntraIDService> _logger;

        public EntraIDService(
            GraphServiceClient graphClient,
            MicrosoftIdentityConsentAndConditionalAccessHandler consentHandler,
            IHttpContextAccessor httpContextAccessor,
            ITokenAcquisition tokenAcquisition,
            ILogger<EntraIDService> logger)
        {
            _graphClient = graphClient ?? throw new ArgumentNullException(nameof(graphClient));
            _consentHandler = consentHandler ?? throw new ArgumentNullException(nameof(consentHandler));
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
            _tokenAcquisition = tokenAcquisition ?? throw new ArgumentNullException(nameof(tokenAcquisition));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<(BomberoViweModel? bombero, ImagenResultado? foto)> BuscarPorUPNAsync(string upn, CancellationToken token)
        {
            if (string.IsNullOrWhiteSpace(upn))
                throw new ArgumentException("El UPN no puede estar vacío.");

            // Verificar autenticación primero
            if (!IsUserAuthenticated())
            {
                throw new InvalidOperationException("Usuario no autenticado. Por favor, inicie sesión.");
            }

            string fullUpn = upn.Contains("@") ? upn : $"{upn}@bomberoschivilcoy.org.ar";

            try
            {
                _logger.LogInformation("Buscando usuario con UPN: {Upn}", fullUpn);

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
                {
                    _logger.LogWarning("No se encontró usuario con UPN: {Upn}", fullUpn);
                    return (null, null);
                }

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
                        fotoBytes = ms.ToArray();

                        contentType = photoMetadata.AdditionalData?.ContainsKey("@odata.mediaContentType") == true
                            ? photoMetadata.AdditionalData["@odata.mediaContentType"]?.ToString()
                            : "image/jpeg";
                    }
                }
                catch (ServiceException photoEx) when (photoEx.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    _logger.LogInformation("Usuario {Upn} no tiene foto de perfil", fullUpn);
                }

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

                _logger.LogInformation("Usuario encontrado exitosamente: {Nombre} {Apellido}", bomberoView.Nombre, bomberoView.Apellido);
                return (bomberoView, foto);
            }
            catch (MicrosoftIdentityWebChallengeUserException ex)
            {
                _logger.LogError(ex, "Se requiere consentimiento del usuario para acceder a Graph");
                _consentHandler.HandleException(ex);
                throw;
            }
            catch (MsalUiRequiredException ex)
            {
                _logger.LogError(ex, "Se requiere interacción del usuario para obtener el token");
                throw new InvalidOperationException("Se requiere iniciar sesión nuevamente. Por favor, cierre sesión y vuelva a ingresar.", ex);
            }
            catch (ServiceException ex) when (ex.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                _logger.LogError(ex, "Token inválido o expirado");
                throw new InvalidOperationException("Su sesión ha expirado. Por favor, cierre sesión y vuelva a ingresar.", ex);
            }
            catch (ServiceException ex)
            {
                _logger.LogError(ex, "Error de servicio de Graph: {Message}", ex.Message);
                throw new InvalidOperationException($"Error al comunicarse con Microsoft Graph: {ex.Message}", ex);
            }
        }

        public async Task<bool> CheckDisponibilidadAsync()
        {
            try
            {
                var httpContext = _httpContextAccessor.HttpContext;

                if (httpContext == null)
                {
                    _logger.LogError("HttpContext es null en CheckDisponibilidadAsync");
                    return false;
                }

                var user = httpContext.User;

                if (user?.Identity?.IsAuthenticated != true)
                {
                    _logger.LogWarning("Usuario no autenticado. Identity: {Identity}, IsAuth: {IsAuth}",
                        user?.Identity?.Name,
                        user?.Identity?.IsAuthenticated);
                    return false;
                }

                _logger.LogInformation("Usuario autenticado: {User}, Claims: {ClaimCount}",
                    user.Identity.Name,
                    user.Claims.Count());

                // Intentar obtener información del usuario actual primero (no requiere permisos adicionales)
                var me = await _graphClient.Me
                    .Request()
                    .Select("id,displayName")
                    .GetAsync();

                if (me != null)
                {
                    _logger.LogInformation("Graph API disponible. Usuario: {DisplayName}", me.DisplayName);
                    return true;
                }

                return false;
            }
            catch (MsalUiRequiredException ex)
            {
                _logger.LogWarning(ex, "Se requiere consentimiento del usuario. ErrorCode: {ErrorCode}", ex.ErrorCode);

                // Si es user_null, el problema es de autenticación
                if (ex.ErrorCode == "user_null")
                {
                    _logger.LogError("Error user_null: El usuario no está correctamente autenticado en el contexto");
                }

                return false;
            }
            catch (MicrosoftIdentityWebChallengeUserException ex)
            {
                _logger.LogWarning(ex, "Se requiere challenge de usuario");
                throw; // Re-lanzar para que el ConsentHandler lo maneje
            }
            catch (ServiceException ex)
            {
                _logger.LogError(ex, "Error al verificar disponibilidad de Graph: {StatusCode}, {ErrorCode}",
                    ex.StatusCode,
                    ex.Error?.Code);
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al verificar disponibilidad de Graph");
                return false;
            }
        }

        public async Task<User?> GetUserAsync()
        {
            try
            {
                if (!IsUserAuthenticated())
                {
                    _logger.LogWarning("Intento de obtener usuario sin autenticación");
                    return null;
                }

                // Usar el GraphClient ya configurado en lugar de crear uno nuevo
                var user = await _graphClient.Me
                    .Request()
                    .GetAsync();

                _logger.LogInformation("Usuario obtenido exitosamente: {Upn}", user?.UserPrincipalName);
                return user;
            }
            catch (MsalUiRequiredException ex)
            {
                _logger.LogError(ex, "Token expirado al obtener usuario");
                throw new InvalidOperationException("Se requiere iniciar sesión nuevamente.", ex);
            }
            catch (ServiceException ex)
            {
                _logger.LogError(ex, "Error de Graph al obtener usuario: {StatusCode}", ex.StatusCode);
                throw new InvalidOperationException($"Error al obtener información del usuario: {ex.Message}", ex);
            }
        }

        private bool IsUserAuthenticated()
        {
            var user = _httpContextAccessor.HttpContext?.User;
            bool isAuthenticated = user?.Identity?.IsAuthenticated == true;

            if (!isAuthenticated)
            {
                _logger.LogWarning("Usuario no autenticado en HttpContext");
            }

            return isAuthenticated;
        }
    }
}