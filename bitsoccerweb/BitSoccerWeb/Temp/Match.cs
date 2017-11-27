using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace BitSoccerWeb.Temp
{
    public class Match
    {
        [XmlAttribute]
        public string Team1Name { get; set; }

        [XmlAttribute]
        public string Team2Name { get; set; }

        public List<SerializableGameState> GameStates { get; set; }

        public Match()
        {
            GameStates = new List<SerializableGameState>();
        }
    }
}