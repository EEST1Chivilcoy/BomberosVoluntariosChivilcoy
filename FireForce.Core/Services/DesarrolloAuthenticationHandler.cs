using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace FireForce.Core.Services
{
    /// <summary>
    /// Esquema de autenticación para trabajar sin Entra ID.
    /// Autentica automáticamente a un usuario ficticio con todos los roles, de modo que
    /// <c>[Authorize]</c>, <c>AuthorizeView</c> y las políticas de autorización siguen
    /// funcionando exactamente igual, pero sin depender de Microsoft Entra ID.
    /// </summary>
    /// <remarks>
    /// Se activa con la clave de configuración <c>Autenticacion:Deshabilitada</c>.
    /// Está pensado solo para desarrollo: en producción esa bandera debe quedar en <c>false</c>.
    /// </remarks>
    public class DesarrolloAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        /// <summary>
        /// Nombre del esquema de autenticación de desarrollo.
        /// </summary>
        public const string Esquema = "DesarrolloSinEntraID";

        public DesarrolloAuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder)
            : base(options, logger, encoder)
        {
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, "Usuario de Desarrollo"),
                new Claim(ClaimTypes.NameIdentifier, "desarrollo"),
                new Claim("preferred_username", "desarrollo@bomberoschivilcoy.org.ar"),
                new Claim(ClaimTypes.Role, "administrador"),
                new Claim(ClaimTypes.Role, "secretaria")
            };

            var identidad = new ClaimsIdentity(claims, Esquema, ClaimTypes.Name, ClaimTypes.Role);
            var usuario = new ClaimsPrincipal(identidad);
            var ticket = new AuthenticationTicket(usuario, Esquema);

            return Task.FromResult(AuthenticateResult.Success(ticket));
        }
    }
}
