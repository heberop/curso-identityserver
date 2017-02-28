using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using IdentityModel.Client;

namespace MvcClient.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        [Authorize]
        public async Task<IActionResult> Contact()
        {
            var accessToken = await HttpContext.Authentication.GetTokenAsync("access_token");
            var refreshToken = await HttpContext.Authentication.GetTokenAsync("refresh_token");
            ViewData["Message"] = "Your contact page.";

            var newToken = (await RefreshToken(refreshToken)).AccessToken;

            return View();
        }

        private async Task<TokenResponse> RefreshToken(string refreshToken)
        {
            var client = new TokenClient("http://localhost:5000/connect/token", "mvc-client", "segredo");
            var response = await client.RequestRefreshTokenAsync(refreshToken);

            return response;
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
