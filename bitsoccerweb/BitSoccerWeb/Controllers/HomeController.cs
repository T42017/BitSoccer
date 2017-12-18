using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;


namespace BitSoccerWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly DropDownViewModel _dropDownViewModel = new DropDownViewModel();
        public HomeController()
        {
            
        }
            

        public IActionResult Index()
        {
            return View(_dropDownViewModel);
        }

        public IActionResult Simulate()
        {
            ViewData["Message"] = "Simulations";

            string teamOneName = "TeamMeh";
            string teamTwoName = "TeamScania";
            string savePath = $"{DateTime.Now.ToString("yyMMddhhmmss")}-{teamOneName}-{teamTwoName}";


            return View();
        }

        [HttpPost]
        public IActionResult TwoTeamSim()
        {

            var projectFolder = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            var teamOnePath = @"\bitsoccerweb\BitSoccerWeb\Teams\TeamScania.dll";
            var teamOne = Path.Combine(projectFolder + teamOnePath);

            var teamTwoPath = @"\bitsoccerweb\BitSoccerWeb\Teams\TeamOskar.dll";
            var teamTwo = Path.Combine(projectFolder + teamTwoPath);

            var teamOneName = teamOnePath.Split(@"Teams\");
            var teamTwoName = teamTwoPath.Split(@"Teams\");

            var t1Name = teamOneName[1].Split(".dll");
            var t2Name = teamTwoName[1].Split(".dll");

            var matchPath = $@"\bitsoccerweb\BitSoccerWeb\Matches\{DateTime.Now.ToString("yyMMddhhmmss")}-{t1Name[0]}-{t2Name[0]}.xml";

            var matches = Path.Combine(projectFolder + matchPath);


            MatchManager.PlayMatch(teamOne, teamTwo, matches);




            var document = XDocument.Load(matches);
            var resultFromXml = document.XPathSelectElements("//SerializableGameState").Last().FirstAttribute.Value + " - " +
                                document.XPathSelectElements("//SerializableGameState").Last().LastAttribute.Value;




            string result = resultFromXml;
            
            ViewBag.Result = result;

            return View("../Home/Simulate");
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
