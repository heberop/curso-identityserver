using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MvcAppDemo.Controllers
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
        public async Task<IActionResult> Secure()
        {
            ViewData["Message"] = "Secured page";
            ViewBag.AccessToken = await HttpContext.Authentication.GetTokenAsync("access_token");

            return View();
        }

        [Authorize]
        public async Task<IActionResult> Api()
        {
            var accessToken = await HttpContext.Authentication.GetTokenAsync("access_token");

            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var response = await client.GetAsync("http://localhost:9090/api/values");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                ViewData["result"] = json;
            }
            else
            {
                ViewData["result"] = "Error:" + response.StatusCode;
            }

            return View("Api");
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
