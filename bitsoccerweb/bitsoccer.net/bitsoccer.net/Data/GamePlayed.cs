using System.Collections.Generic;
using System.Xml.Serialization;

namespace bitsoccer.net.Data
{
    public class GamePlayed
    {
        [XmlAttribute]
        public string Team1Name { get; set; }

        [XmlAttribute]
        public string Team2Name { get; set; }

        public List<SerializableGameState> States { get; set; }

        public GamePlayed()
        {
            States = new List<SerializableGameState>();
        }
    }
}