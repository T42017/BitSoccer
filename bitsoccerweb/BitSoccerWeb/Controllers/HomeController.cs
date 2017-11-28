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
using BitSoccerWeb.Data;


namespace BitSoccerWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly DropDownViewModel _dropDownViewModel = new DropDownViewModel(); // Skapa en ny instans av DropDownViewModel (modell)
        
        public HomeController()
        {
            
        }

        public IActionResult Index()
        {
            return View(_dropDownViewModel); // Skicka med modellen till View:n för att få tillgång till dess saker (Teams, t.ex.)
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Simulate()
        {
            ViewData["Message"] = "Simulations";

            MatchManager.PlayMatch(
                @"C:\Users\usr\Documents\Git\12-asp-bitsoccer\BitSoccer\bitsoccerweb\BitSoccerWeb\Teams\TeamOne.dll",
                @"C:\Users\usr\Documents\Git\12-asp-bitsoccer\BitSoccer\bitsoccerweb\BitSoccerWeb\Teams\TeamLomma.dll",
                $@"C:\Users\usr\Documents\Git\12-asp-bitsoccer\BitSoccer\bitsoccerweb\BitSoccerWeb\Matches\{Guid.NewGuid()}.xml",
                1
            );

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
