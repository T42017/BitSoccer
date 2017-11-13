using System.Xml.Serialization;

namespace bitsoccer.net.Data
{
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

            ret.Team1Goal = state.GetScoreInfo().Team1;
            ret.Team2Goal = state.GetScoreInfo().Team2;

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
}