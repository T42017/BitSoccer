﻿using System;
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



            return View();
        }

        [HttpPost]
        public IActionResult TwoTeamSim(string NrOfMatches)
        {
            
            var nrOfMatches = int.Parse(NrOfMatches);

            List<(string, string, int)> info = new List<ValueTuple<string, string, int>>();

            for (int i = 1; i < nrOfMatches+1; i++)
            {
                var projectFolder = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
                var teamOnePath = @"\bitsoccerweb\BitSoccerWeb\Teams\TeamScania.dll";
                var teamOne = Path.Combine(projectFolder + teamOnePath);

                var teamTwoPath = @"\bitsoccerweb\BitSoccerWeb\Teams\TeamOskar.dll";
                var teamTwo = Path.Combine(projectFolder + teamTwoPath);

                var firstSplit = teamOnePath.Split(@"Teams\");
                var secondSplit = teamTwoPath.Split(@"Teams\");

                var lastSplitTeamOne = firstSplit[1].Split(".dll");
                var lastSplitTeamTwo = secondSplit[1].Split(".dll");

                var matchPath = $@"\bitsoccerweb\BitSoccerWeb\Matches\{Guid.NewGuid()}.xml";

                var matches = Path.Combine(projectFolder + matchPath);
                

                MatchManager.PlayMatch(teamOne, teamTwo, matches);
                var document = XDocument.Load(matches);
                var teamOneScore = document.XPathSelectElements("//SerializableGameState").Last().FirstAttribute.Value;
                var teamTwoScore = document.XPathSelectElements("//SerializableGameState").Last().LastAttribute.Value;
                var matchSeed = document.XPathSelectElements("//Match").First().FirstAttribute.NextAttribute.NextAttribute.Value;
                var teams = lastSplitTeamOne[0] + " " + " - " + " " + lastSplitTeamTwo[0];
                var result = teamOneScore + " - " + teamTwoScore;
                info.Add((result, matchSeed, i));

                ViewBag.List = info;
                ViewBag.Teams = teams;
                
            }
            
            ViewBag.NrOfMatches = nrOfMatches;
            return View("../Home/Simulate");
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
