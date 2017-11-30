using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BitSoccerWeb.Models;
using BitSoccerWeb.Models.HomeViewModels;
using BitSoccerWeb.Temp;
using Common;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;


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

        public IActionResult Simulate()
        {
            ViewData["Message"] = "Simulations";

            string teamOneName = "TeamMeh";
            string teamTwoName = "TeamScania";
            string savePath = $"{DateTime.Now.ToString("yyMMddhhmmss")}-{teamOneName}-{teamTwoName}";

            MatchManager.PlayMatch(
                $@"C:\Users\usr\Documents\Git\12-asp-bitsoccer\BitSoccer\bitsoccerweb\BitSoccerWeb\Teams\{teamOneName}.dll",
                $@"C:\Users\usr\Documents\Git\12-asp-bitsoccer\BitSoccer\bitsoccerweb\BitSoccerWeb\Teams\{teamTwoName}.dll",
                $@"C:\Users\usr\Documents\Git\12-asp-bitsoccer\BitSoccer\bitsoccerweb\BitSoccerWeb\Matches\{savePath}.xml"
            );

            //MatchManager.PlayMatch("~/Teams/ProTeam.dll", "~/Teams/TeamOskar.dll", "~/Matches/");

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
