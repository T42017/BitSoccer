using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace BitSoccerWeb.Temp
{
    public class SerializableGameState
    {
        public SerializableVector BallPosition { get; set; }

        [XmlElement("Team1Player1")]
        public SerializableVector Team1Player1 { get; set; }
        [XmlElement("Team1Player2")]
        public SerializableVector Team1Player2 { get; set; }
        [XmlElement("Team1Player3")]
        public SerializableVector Team1Player3 { get; set; }
        [XmlElement("Team1Player4")]
        public SerializableVector Team1Player4 { get; set; }
        [XmlElement("Team1Player5")]
        public SerializableVector Team1Player5 { get; set; }
        [XmlElement("Team1Player6")]
        public SerializableVector Team1Player6 { get; set; }

        [XmlElement("Team2Player1")]
        public SerializableVector Team2Player1 { get; set; }
        [XmlElement("Team2Player2")]
        public SerializableVector Team2Player2 { get; set; }
        [XmlElement("Team2Player3")]
        public SerializableVector Team2Player3 { get; set; }
        [XmlElement("Team2Player4")]
        public SerializableVector Team2Player4 { get; set; }
        [XmlElement("Team2Player5")]
        public SerializableVector Team2Player5 { get; set; }
        [XmlElement("Team2Player6")]
        public SerializableVector Team2Player6 { get; set; }

        [XmlAttribute]
        public int Team1Scores { get; set; }

        [XmlAttribute]
        public int Team2Scores { get; set; }

        public static SerializableGameState FromGameState(GameState gameState)
        {
            return new SerializableGameState
            {
                Team1Scores = gameState.GetScoreInfo().Team1,
                Team2Scores = gameState.GetScoreInfo().Team2,

                BallPosition = SerializableVector.FromVector(gameState.BallInfo.Position),
                
                Team1Player1 = SerializableVector.FromVector(gameState.Teams()[0].GetPlayers()[0].GetPosition()),
                Team1Player2 = SerializableVector.FromVector(gameState.Teams()[0].GetPlayers()[1].GetPosition()),
                Team1Player3 = SerializableVector.FromVector(gameState.Teams()[0].GetPlayers()[2].GetPosition()),
                Team1Player4 = SerializableVector.FromVector(gameState.Teams()[0].GetPlayers()[3].GetPosition()),
                Team1Player5 = SerializableVector.FromVector(gameState.Teams()[0].GetPlayers()[4].GetPosition()),
                Team1Player6 = SerializableVector.FromVector(gameState.Teams()[0].GetPlayers()[5].GetPosition()),
                
                Team2Player1 = SerializableVector.FromVector(gameState.Teams()[1].GetPlayers()[0].GetPosition()),
                Team2Player2 = SerializableVector.FromVector(gameState.Teams()[1].GetPlayers()[1].GetPosition()),
                Team2Player3 = SerializableVector.FromVector(gameState.Teams()[1].GetPlayers()[2].GetPosition()),
                Team2Player4 = SerializableVector.FromVector(gameState.Teams()[1].GetPlayers()[3].GetPosition()),
                Team2Player5 = SerializableVector.FromVector(gameState.Teams()[1].GetPlayers()[4].GetPosition()),
                Team2Player6 = SerializableVector.FromVector(gameState.Teams()[1].GetPlayers()[5].GetPosition()),
            };
        }
    }
}
