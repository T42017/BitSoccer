using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BitSoccerWeb.Temp
{
    public class SerializableGameState
    {
        public SerializableVector BallPosition { get; set; }

        //[XmlElement("Team1Positions")]
        //public Dictionary<string, SerializableVector> Team1Positions { get; set; } = new Dictionary<string, SerializableVector>()
        //{     
        //    { "Player1", null },
        //    { "Player2", null },
        //    { "Player3", null },
        //    { "Player4", null },
        //    { "Player5", null },
        //    { "Player6", null }
        //};

        [XmlElement("Team1Positions")]
        public List<SerializableVector> Team1Positions { get; set; } = new List<SerializableVector>() { null, null, null, null, null, null };

        //[XmlElement("Team2Positions")]
        //public Dictionary<string, SerializableVector> Team2Positions { get; set; } = new Dictionary<string, SerializableVector>()
        //{
        //    { "Player1", null },
        //    { "Player2", null },
        //    { "Player3", null },
        //    { "Player4", null },
        //    { "Player5", null },
        //    { "Player6", null }
        //};

        [XmlElement("Team2Positions")]
        public List<SerializableVector> Team2Positions { get; set; } = new List<SerializableVector>() { null, null, null, null, null, null };

        //[XmlAttribute]
        //public Dictionary<string, int> TeamScores { get; set; } = new Dictionary<string, int>()
        //{
        //    { "Team1", 0 },
        //    { "Team2", 0 }
        //};

        [XmlAttribute]
        public List<int> TeamScores { get; set; } = new List<int>() { 0, 0 };

        public static SerializableGameState FromGameState(GameState gameState)
        {
            return new SerializableGameState
            {
                //TeamScores =
                //{
                //    ["Team1"] = gameState.GetScoreInfo().Team1,
                //    ["Team2"] = gameState.GetScoreInfo().Team2
                //},

                TeamScores = new List<int>()
                {
                    gameState.GetScoreInfo().Team1,
                    gameState.GetScoreInfo().Team2
                },

                BallPosition = SerializableVector.FromVector(gameState.BallInfo.Position),

                //Team1Positions =
                //{
                //    ["Player1"] = SerializableVector.FromVector(gameState.Teams()[0].GetPlayers()[0].GetPosition()),
                //    ["Player2"] = SerializableVector.FromVector(gameState.Teams()[0].GetPlayers()[1].GetPosition()),
                //    ["Player3"] = SerializableVector.FromVector(gameState.Teams()[0].GetPlayers()[2].GetPosition()),
                //    ["Player4"] = SerializableVector.FromVector(gameState.Teams()[0].GetPlayers()[3].GetPosition()),
                //    ["Player5"] = SerializableVector.FromVector(gameState.Teams()[0].GetPlayers()[4].GetPosition()),
                //    ["Player6"] = SerializableVector.FromVector(gameState.Teams()[0].GetPlayers()[5].GetPosition()),
                //},

                Team1Positions = new List<SerializableVector>()
                {
                    SerializableVector.FromVector(gameState.Teams()[0].GetPlayers()[0].GetPosition()),
                    SerializableVector.FromVector(gameState.Teams()[0].GetPlayers()[1].GetPosition()),
                    SerializableVector.FromVector(gameState.Teams()[0].GetPlayers()[2].GetPosition()),
                    SerializableVector.FromVector(gameState.Teams()[0].GetPlayers()[3].GetPosition()),
                    SerializableVector.FromVector(gameState.Teams()[0].GetPlayers()[4].GetPosition()),
                    SerializableVector.FromVector(gameState.Teams()[0].GetPlayers()[5].GetPosition()),
                },

                //Team2Positions =
                //{
                //    ["Player1"] = SerializableVector.FromVector(gameState.Teams()[1].GetPlayers()[0].GetPosition()),
                //    ["Player2"] = SerializableVector.FromVector(gameState.Teams()[1].GetPlayers()[1].GetPosition()),
                //    ["Player3"] = SerializableVector.FromVector(gameState.Teams()[1].GetPlayers()[2].GetPosition()),
                //    ["Player4"] = SerializableVector.FromVector(gameState.Teams()[1].GetPlayers()[3].GetPosition()),
                //    ["Player5"] = SerializableVector.FromVector(gameState.Teams()[1].GetPlayers()[4].GetPosition()),
                //    ["Player6"] = SerializableVector.FromVector(gameState.Teams()[1].GetPlayers()[5].GetPosition()),
                //},

                Team2Positions = new List<SerializableVector>()
                {
                    SerializableVector.FromVector(gameState.Teams()[1].GetPlayers()[0].GetPosition()),
                    SerializableVector.FromVector(gameState.Teams()[1].GetPlayers()[1].GetPosition()),
                    SerializableVector.FromVector(gameState.Teams()[1].GetPlayers()[2].GetPosition()),
                    SerializableVector.FromVector(gameState.Teams()[1].GetPlayers()[3].GetPosition()),
                    SerializableVector.FromVector(gameState.Teams()[1].GetPlayers()[4].GetPosition()),
                    SerializableVector.FromVector(gameState.Teams()[1].GetPlayers()[5].GetPosition()),
                },
            };
        }
    }
}
