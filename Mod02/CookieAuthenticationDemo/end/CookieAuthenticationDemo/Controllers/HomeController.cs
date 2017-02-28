using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CookieAuthenticationDemo.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AutoLogin()
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, "Robert"),
                new Claim(ClaimTypes.Surname, "Plant")
            };

            var identity = new ClaimsIdentity(claims);
            var principal = new ClaimsPrincipal(identity);

            HttpContext.Authentication.SignInAsync("Cookies", principal);

            return RedirectToAction("Index");
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

        public IActionResult Error()
        {
            return View();
        }
    }
}
