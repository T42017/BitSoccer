using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using bitsoccer.net.Data;
using Common;
using GameEngine;

namespace bitsoccer.net.HelpClasses
{
    public class Player
    {
        public static void PlayMatchTest(string savePath)
        {
            var engine = (IGameEngine)new GameEngine.GameEngine(new TeamOne.TeamOne(), new TeamTwo.TeamTwo());
            PlayMatch(engine, savePath);
        }

        public static void PlayMatch(string path1, string path2, string filePath)
        {
            var engine = (IGameEngine)new GameEngine.GameEngine(path1, path2);
            PlayMatch(engine, filePath);
        }

        public static string PlayMatch(string path1, string path2)
        {
            var engine = (IGameEngine)new GameEngine.GameEngine(path1, path2);
            return PlayMatch(engine);
        }

        public static string PlayMatch(ITeam team1, ITeam team2)
        {
            var engine = (IGameEngine)new GameEngine.GameEngine(team1, team2);
            return PlayMatch(engine);
        }

        private static string PlayMatch(IGameEngine engine)
        {
            int gameStep = 0;
            GameState gameState = null;

            Common.Global.Random = new Random();
            engine.setTimeout(false);
            var gamePlayed = new GamePlayed();

            gamePlayed.Team1Name = engine.Team1Name();
            gamePlayed.Team2Name = engine.Team2Name();

            while (gameStep < Constants.GameEngineMatchLength)
            {
                gameStep++;
                gamePlayed.States.Add(SerializableGameState.FromGameState(engine.GetNext()));
            }

            return ObjectXmlSerializer<GamePlayed>.Save(gamePlayed);
        }

        private static void PlayMatch(IGameEngine engine, string savePath)
        {
            int gameStep = 0;
            GameState gameState = null;

            Common.Global.Random = new Random();
            engine.setTimeout(false);
            var gamePlayed = new GamePlayed();

            gamePlayed.Team1Name = engine.Team1Name();
            gamePlayed.Team2Name = engine.Team2Name();

            while (gameStep < Constants.GameEngineMatchLength)
            {
                gameStep++;
                gamePlayed.States.Add(SerializableGameState.FromGameState(engine.GetNext()));
            }

            ObjectXmlSerializer<GamePlayed>.Save(gamePlayed, savePath);
        }
    }
}