using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BitSoccerWeb.Models;
using BitSoccerWeb.Models.HomeViewModels;
using Common;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;


namespace BitSoccerWeb.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            //var teams = _customersContext.Teams.OrderBy(t => t.TeamName).Select(x => new { Id = x.Code, Value = x.Name });

            //var model = new DropDownViewModel();
            //model.TeamList = new SelectList(teams, "Id", "Value");

            return View(/*model*/);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Profile()
        { 
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

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
