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