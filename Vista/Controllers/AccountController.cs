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
        /// Cierra sesión de forma local o completa según el parámetro 'local'
        /// </summary>
        /// <param name="local">Si es true, solo cierra la sesión local sin cerrar sesión en Microsoft</param>
        [HttpGet("SignOut")]
        public async Task<IActionResult> SignOut([FromQuery] bool local = false)
        {
            if (local)
            {
                // Cierre de sesión local: solo borra las cookies de la aplicación
                // No cierra la sesión en Microsoft Entra ID
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                
                // Redirige a la página de inicio
                return LocalRedirect("/");
            }
            else
            {
                // Cierre de sesión completo: cierra sesión en la aplicación Y en Microsoft
                var callbackUrl = Url.Page("/Index", pageHandler: null, values: null, protocol: Request.Scheme);
                
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                await HttpContext.SignOutAsync(OpenIdConnectDefaults.AuthenticationScheme, 
                    new AuthenticationProperties 
                    { 
                        RedirectUri = callbackUrl 
                    });
                
                return new EmptyResult();
            }
        }
    }
}
