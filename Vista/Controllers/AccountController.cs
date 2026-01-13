using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Vista.Controllers
{
    [AllowAnonymous]
    [Route("MicrosoftIdentity/Account")]
    public class AccountController : Controller
    {
        /// <summary>
        /// Inicia sesión y redirige al redirectUri especificado
        /// </summary>
        [HttpGet("SignIn")]
        public IActionResult SignIn([FromQuery] string? redirectUri)
        {
            // Si no hay redirectUri o está vacío, usar /fire-force/ como predeterminado
            if (string.IsNullOrWhiteSpace(redirectUri))
            {
                redirectUri = "/fire-force/";
            }

            // Si el redirectUri es la raíz o el inicio público, redirigir al sistema
            var uri = Uri.TryCreate(redirectUri, UriKind.RelativeOrAbsolute, out var parsedUri) ? parsedUri : null;
            var path = uri?.IsAbsoluteUri == true ? uri.AbsolutePath : redirectUri;

            if (path == "/" || path == "/contacto" || path == "/cuerpo-activo" || path == "/historia")
            {
                redirectUri = "/fire-force/";
            }

            return Challenge(
                new AuthenticationProperties
                {
                    RedirectUri = redirectUri
                },
                OpenIdConnectDefaults.AuthenticationScheme
            );
        }

        /// <summary>
        /// Cierra sesion de forma completa
        /// </summary>
        [HttpGet("SignOut")]
        public IActionResult SignOut([FromQuery] bool local = false)
        {
            var callbackUrl = Url.Action("Index", "Home", values: null, protocol: Request.Scheme);

            return SignOut(
                new AuthenticationProperties
                {
                    RedirectUri = callbackUrl
                },
                CookieAuthenticationDefaults.AuthenticationScheme,
                OpenIdConnectDefaults.AuthenticationScheme
            );
        }
    }
}