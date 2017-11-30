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
using System.Xml.Linq;
using System.Xml.XPath;


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

            //var projectFolder = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            //var teamOnePath = @"\bitsoccerweb\BitSoccerWeb\Teams\TeamOne.dll";
            //var teamOne = Path.Combine(projectFolder + teamOnePath);

            //var teamTwoPath = @"\bitsoccerweb\BitSoccerWeb\Teams\TeamLomma.dll";
            //var teamTwo = Path.Combine(projectFolder + teamTwoPath);

            //var matchPath = $@"\bitsoccerweb\BitSoccerWeb\Matches\{Guid.NewGuid()}.xml";
            //var matches = Path.Combine(projectFolder + matchPath);
            //// C:\GIT\BitSoccerWeb\bitsoccerweb\BitSoccerWeb\Matches
            //MatchManager.PlayMatch(
            //        teamOne,
            //        teamTwo,
            //        matches,
            //        1
            //    );


            return View();
        }

        [HttpPost]
        public IActionResult TwoTeamSim()
        {
            
            var projectFolder = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            var teamOnePath = @"\bitsoccerweb\BitSoccerWeb\Teams\TeamOne.dll";
            var teamOne = Path.Combine(projectFolder + teamOnePath);

            var teamTwoPath = @"\bitsoccerweb\BitSoccerWeb\Teams\TeamLomma.dll";
            var teamTwo = Path.Combine(projectFolder + teamTwoPath);

            var matchPath = $@"\bitsoccerweb\BitSoccerWeb\Matches\{Guid.NewGuid()}.xml";
            var matches = Path.Combine(projectFolder + matchPath);
            var mPath = @"\bitsoccerweb\\BitSoccerWeb\Matches\f690a309-7bd6-4958-bb7f-47b943e8a099.xml";
            var match2 = Path.Combine(projectFolder + mPath);
           

            MatchManager.PlayMatch(teamOne,teamTwo,matches);



            //var document = XDocument.Load(match2);
            //var root = document.Root;
            //var lastElement = (XElement) root.;

            //var document = XDocument.Load(match2);
            //var text = document.XPathSelectElements("//@TeamScores");

            //Debug.WriteLine(text.ToString());


            return View("../Home/Simulate");

        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
