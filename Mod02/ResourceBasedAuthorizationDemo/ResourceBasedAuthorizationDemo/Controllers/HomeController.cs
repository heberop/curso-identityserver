using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ResourceBasedAuthorizationDemo.Policies;

namespace ResourceBasedAuthenticationDemo.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAuthorizationService authorizationService;
        public HomeController(IAuthorizationService authorizationService)
        {
            this.authorizationService = authorizationService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Orders(int id)
        {
            var order = Data.Orders.Get(1);
            if(await authorizationService
                .AuthorizeAsync(User, order, new OnlyOwnOrdersRequirement()))
            {
                return Forbid();
            }

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

        public IActionResult Error()
        {
            return View();
        }
    }
}
