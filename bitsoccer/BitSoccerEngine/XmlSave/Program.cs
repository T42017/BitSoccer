using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Common;
using GameEngine;

namespace XmlSave
{
    public class GamePlayed
    {
        public List<SerializableGameState> States { get; set; }

        public GamePlayed()
        {
            States = new List<SerializableGameState>();
        }
    }

    public class SerializableVector
    {
        [XmlAttribute]
        public int X { get; set; }
        [XmlAttribute]
        public int Y { get; set; }

        public static SerializableVector FromVector(Vector vector)
        {
            var ret = new SerializableVector
            {
                X = (int)vector.X,
                Y = (int)vector.Y
            };
            return ret;
        }
    }

    public class SerializableGameState
    {
        public SerializableVector BallPosition { get; set; }

        [XmlElement("T1P1")]
        public SerializableVector Team1P1Position { get; set; }
        [XmlElement("T1P2")]
        public SerializableVector Team1P2Position { get; set; }
        [XmlElement("T1P3")]
        public SerializableVector Team1P3Position { get; set; }
        [XmlElement("T1P4")]
        public SerializableVector Team1P4Position { get; set; }
        [XmlElement("T1P5")]
        public SerializableVector Team1P5Position { get; set; }
        [XmlElement("T1P6")]
        public SerializableVector Team1P6Position { get; set; }

        [XmlElement("T2P1")]
        public SerializableVector Team2P1Position { get; set; }
        [XmlElement("T2P2")]
        public SerializableVector Team2P2Position { get; set; }
        [XmlElement("T2P3")]
        public SerializableVector Team2P3Position { get; set; }
        [XmlElement("T2P4")]
        public SerializableVector Team2P4Position { get; set; }
        [XmlElement("T2P5")]
        public SerializableVector Team2P5Position { get; set; }
        [XmlElement("T2P6")]
        public SerializableVector Team2P6Position { get; set; }

        [XmlAttribute]
        public int Team1Goal { get; set; }
        [XmlAttribute]
        public int Team2Goal { get; set; }

        public static SerializableGameState FromGameState(GameState state)
        {
            var ret = new SerializableGameState();

            ret.BallPosition = SerializableVector.FromVector(state.BallInfo.Position);
            ret.Team1P1Position = SerializableVector.FromVector(state.Teams()[0].GetPlayers()[0].GetPosition());
            ret.Team1P2Position = SerializableVector.FromVector(state.Teams()[0].GetPlayers()[1].GetPosition());
            ret.Team1P3Position = SerializableVector.FromVector(state.Teams()[0].GetPlayers()[2].GetPosition());
            ret.Team1P4Position = SerializableVector.FromVector(state.Teams()[0].GetPlayers()[3].GetPosition());
            ret.Team1P5Position = SerializableVector.FromVector(state.Teams()[0].GetPlayers()[4].GetPosition());
            ret.Team1P6Position = SerializableVector.FromVector(state.Teams()[0].GetPlayers()[5].GetPosition());

            ret.Team2P1Position = SerializableVector.FromVector(state.Teams()[1].GetPlayers()[0].GetPosition());
            ret.Team2P2Position = SerializableVector.FromVector(state.Teams()[1].GetPlayers()[1].GetPosition());
            ret.Team2P3Position = SerializableVector.FromVector(state.Teams()[1].GetPlayers()[2].GetPosition());
            ret.Team2P4Position = SerializableVector.FromVector(state.Teams()[1].GetPlayers()[3].GetPosition());
            ret.Team2P5Position = SerializableVector.FromVector(state.Teams()[1].GetPlayers()[4].GetPosition());
            ret.Team2P6Position = SerializableVector.FromVector(state.Teams()[1].GetPlayers()[5].GetPosition());

            return ret;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            int gameStep = 0;
            GameState gameState = null;

            Common.Global.Random = new Random();
            var _engine = (IGameEngine)new GameEngine.GameEngine(new TeamOne.TeamOne(), new TeamTwo.TeamTwo());
            _engine.setTimeout(false);
            var gamePlayed = new GamePlayed();

            while (gameStep < Constants.GameEngineMatchLength)
            {
                gameStep++;
                gamePlayed.States.Add(SerializableGameState.FromGameState(_engine.GetNext()));
            }
            
            XMLSerialisering.ObjectXmlSerializer<GamePlayed>.Save(gamePlayed, "test.xml");
        }
    }
}
