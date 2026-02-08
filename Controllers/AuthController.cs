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

            var email = result.Principal.FindFirst(System.Security.Claims.ClaimTypes.Email)?.Value;
            var name = result.Principal.FindFirst(System.Security.Claims.ClaimTypes.Name)?.Value;
            var googleId = result.Principal.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            var apiUrl = $"{_authSettings.APIRpoperties.BaseUrl}" +
                 $"{_authSettings.APIRpoperties.Endpoints.Google.Login}";

            using var client = new HttpClient();

            var request = new
            {
                Email = email,
                Name = name,
                GoogleId = googleId
            };

            var response = await client.PostAsJsonAsync(apiUrl, request);

            if (!response.IsSuccessStatusCode)
                return Unauthorized();

            var tokenResponse = await response.Content.ReadFromJsonAsync<TokenResponse>();

            // Store JWT token
            HttpContext.Session.SetString("JWToken", tokenResponse.Token);

            return RedirectToAction("Index", "Home");
        }
    }
}
