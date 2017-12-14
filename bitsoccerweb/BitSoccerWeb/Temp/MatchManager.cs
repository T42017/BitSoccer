using System;
using Common;
using GameEngine;

namespace BitSoccerWeb.Temp
{
    public static class MatchManager
    {
        public static void PlayMatch(string team1Path, string team2Path, string savePath, int? matchSeed = null)
        {
            //var gameEngine = (IGameEngine) new GameEngine.GameEngine(team1Path, team2Path);
            var gameEngine = new GameEngine.GameEngine(team1Path, team2Path);
            PlayMatch(gameEngine, savePath, matchSeed);
        }
        
        private static void PlayMatch(IGameEngine gameEngine, string savePath, int? matchSeed)
        {
            int gameStep = 0;

            Global.Random = matchSeed == null ? 
                            new Random() : 
                            new Random((int) matchSeed);
            gameEngine.setTimeout(false);

            var match = new Match
            {
                Team1Name = gameEngine.Team1Name(),
                Team2Name = gameEngine.Team2Name(),
                MatchSeed = Global.Random.Next(int.MaxValue)
            };
            while (gameStep < Constants.GameEngineMatchLength)
            {
                gameStep++;
                match.GameStates.Add(SerializableGameState.FromGameState(gameEngine.GetNext()));
            }

            ObjectXmlSerializer<Match>.Save(match, savePath);
        }
    }
}