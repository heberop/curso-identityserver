using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using IdentityModel.Client;

namespace MvcClientCode.Controllers
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

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        [Authorize]
        public IActionResult Secure()
        {
            return View();
        }

        [Authorize]
        public IActionResult ShowClaims()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> ShowTokens()
        {
            ViewBag.AccessToken = await HttpContext.Authentication.GetTokenAsync("access_token");
            ViewBag.RefreshToken = await HttpContext.Authentication.GetTokenAsync("refresh_token");
            return View();
        }
        private async void Refresh(string refreshToken)
        {
            var newToken = (await RefreshToken(refreshToken)).AccessToken;
        }

        private async Task<TokenResponse> RefreshToken(string refreshToken)
        {
            var client = new TokenClient("http://localhost:5000/connect/token", 
                "mvc-client-code", "segredo");
            var response = await client.RequestRefreshTokenAsync(refreshToken);

            return response;
        }

        public IActionResult Logout()
        {
            return new SignOutResult(new string[] { "oidc", "Cookies" });
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
