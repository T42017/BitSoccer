using Common;
using GameEngine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace Simulator
{
    class Program
    {
        static void Main(string[] args)
        {
            Random rnd = new Random();
            if (args.Length != 3)
            {
                Console.WriteLine("usage: inputFolder outputFolder numberOfMatches");
                return;
            }

            string path1 = args[0];
            string path2 = args[1];

            int mathes = int.Parse(args[2]);

            DirectoryInfo di = new DirectoryInfo(path1);
            List<Result> results = new List<Result>();

            foreach (var fileInfo in di.GetFiles("*.dll"))
            {
                Console.WriteLine("[Found]: " + fileInfo.FullName);
                results.Add(new Result() { Name = fileInfo.Name, Dll = fileInfo.FullName });
            }

            for (int i = 0; i < results.Count; i++)
            {

                for (int j = i + 1; j < results.Count; j++)
                {
                    var team1Path = results[i].Dll;
                    var team2Path = results[j].Dll;
                    var _gameEngine = (IGameEngine)new GameEngine.GameEngine(team1Path, team2Path);

                    string matchName = string.Format("{0} vs {1}", _gameEngine.Team1Name(), _gameEngine.Team2Name());
                    Console.WriteLine("\n");

                    Console.BackgroundColor = ConsoleColor.DarkBlue;
                    Console.WriteLine(matchName);
                    Console.BackgroundColor = ConsoleColor.Black;
                    //Console.ReadKey();

                    List<MatchInfo> _infos = new List<MatchInfo>();

                    for (int m = 0; m < mathes; m++)
                    {
                        var matchInfo = new MatchInfo();
                        matchInfo.Seed = rnd.Next();
                        int gameStep = 0;
                        GameState gameState = null;

                        Common.Global.Random = new Random(matchInfo.Seed);
                        var _engine = (IGameEngine)new GameEngine.GameEngine(results[i].Dll, results[j].Dll);
                        _engine.setTimeout(false);

                        while (gameStep < Constants.GameEngineMatchLength)
                        {
                            gameStep++;
                            gameState = _engine.GetNext();
                        }

                        var gameInfo = gameState.GetScoreInfo();
                        matchInfo.Team1Goal = gameInfo.Team1;
                        matchInfo.Team2Goal = gameInfo.Team2;
                        _infos.Add(matchInfo);

                        if (gameInfo.Team1 > gameInfo.Team2)
                        {
                            results[i].Wins++;
                            results[j].Loss++;
                        }
                        else if (gameInfo.Team1 == gameInfo.Team2)
                        {
                            results[i].Draws++;
                            results[j].Draws++;
                        }
                        else
                        {
                            results[i].Loss++;
                            results[j].Wins++;
                        }

                        Console.WriteLine("{0} - {1}", gameInfo.Team1, gameInfo.Team2);

                        _engine.Dispose();
                        _engine = null;
                    }

                    var bestTeam1 = _infos.OrderByDescending(mi => mi.Team1Delta).First();
                    var bestTeam2 = _infos.OrderByDescending(mi => mi.Team2Delta).First();

                    Console.BackgroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine("Best match {0}: {1} - {2} (seed: {3})",
                        _gameEngine.Team1Name(), bestTeam1.Team1Goal, bestTeam1.Team2Goal, bestTeam1.Seed);
                    Console.WriteLine("Best match {0}: {1} - {2} (seed: {3})",
                        _gameEngine.Team2Name(), bestTeam2.Team2Goal, bestTeam2.Team1Goal, bestTeam2.Seed);
                    
                    int team1Pts = 0;
                    int team2Pts = 0;
                    foreach (var matchInfo in _infos)
                    {
                        if (matchInfo.Team1Goal > matchInfo.Team2Goal)
                            team1Pts += 3;
                        else if (matchInfo.Team1Goal == matchInfo.Team2Goal)
                        {
                            team1Pts++;
                            team2Pts++;
                        }
                        else
                            team2Pts += 3;
                    }

                    Console.BackgroundColor = ConsoleColor.DarkMagenta;
                    Console.WriteLine("{0,-40} earned {1} points!", _gameEngine.Team1Name(), team1Pts);
                    Console.WriteLine("{0,-40} earned {1} points!", _gameEngine.Team2Name(), team2Pts);
                    Console.BackgroundColor = ConsoleColor.Black;

                }
            }

            Console.WriteLine("\n\n");

            Console.WriteLine("{0,-50}{1,-8}{2,-8}{3,-8}{4}", "Name", "Wins", "Draws", "Loss", "Points");
            Console.ForegroundColor = ConsoleColor.Green;

            foreach (var source in results.OrderByDescending(team => team.Points))
            {
                Console.WriteLine("{0,-50}{1,-8}{2,-8}{3,-8}{4}", source.Name, source.Wins, source.Draws, source.Loss, source.Points);
                Console.ForegroundColor = ConsoleColor.Gray;
            }

            Console.ReadLine();
        }
    }

    class Result
    {
        public string Name { get; set; }
        public int Wins { get; set; }
        public int Draws { get; set; }
        public int Loss { get; set; }

        public int Points { get { return Wins*3 + Draws; } }
        public string Dll { get; set; }
    }

    class MatchInfo
    {
        public int Team1Goal { get; set; }
        public int Team2Goal { get; set; }
        public int Seed { get; set; }

        public int Team1Delta { get { return Team1Goal - Team2Goal; } }
        public int Team2Delta { get { return Team2Goal - Team1Goal; } }
    }
}
