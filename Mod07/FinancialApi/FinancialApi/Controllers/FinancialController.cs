using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace FinancialApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class FinancialController : Controller
    {
        // GET api/values
        [HttpGet]
        public IActionResult Get()
        {
            return new JsonResult(User.Claims.Select(
                c => new { c.Type, c.Value }));
        }
    }
}
