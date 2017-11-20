using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BitSoccerWeb.Models;
using Common;


namespace BitSoccerWeb.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(string checkbox)
        {
            return Content($"Is it on? It is: {checkbox}");
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Profile()
            ViewData["Message"] = "Profile page.";

            return View();
        }

        public IActionResult Replay()
        {
            ViewData["Message"] = "Replays.";

            return View();
        }

        public IActionResult Simulate()
        {
            ViewData["Message"] = "Replays.";
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
