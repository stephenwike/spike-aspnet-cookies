using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DotnetCookieSpike.Models;
using Microsoft.AspNetCore.Http;

namespace DotnetCookieSpike.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            if (Request.Cookies["name"] != null)
            {
                ViewBag.message = Request.Cookies["name"];
            }
            else
            {
                ViewBag.message = "Not available";
            }

            return View();
        }

        [HttpPost]
        public IActionResult Form(IFormCollection formCollection)
        {
            CookieOptions options = new CookieOptions()
            {
                Expires = DateTime.Now.AddMinutes(2),
                HttpOnly = true,
            };
            Response.Cookies.Append("name", formCollection["txtCookie"], options);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
