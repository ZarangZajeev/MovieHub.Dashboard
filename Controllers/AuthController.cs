using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MovieHub.Dashboard.Models;

namespace MovieHub.Dashboard.Controllers
{
    public class AuthController : Controller
    {
        private readonly AuthenticationSettings _authSettings;

        public AuthController(IOptions<AuthenticationSettings> authSettings)
        {
            _authSettings = authSettings.Value;
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult StartLogin()
        {
            switch (_authSettings.ActiveProvider)
            {
                case "Google":
                    return GoogleLogin();

                default:
                    return BadRequest("Login provider not supported.");
            }
        }

        private IActionResult GoogleLogin()
        {
            var properties = new AuthenticationProperties
            {
                RedirectUri = Url.Action("GoogleResponse")
            };

            return Challenge(properties, GoogleDefaults.AuthenticationScheme);
        }

        public async Task<IActionResult> GoogleResponse()
        {
            var result = await HttpContext.AuthenticateAsync();

            var claims = result.Principal.Identities
                .FirstOrDefault()?.Claims
                .Select(c => new
                {
                    c.Type,
                    c.Value
                });

            return Json(claims);
        }
    }
}
