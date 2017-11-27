using System.Xml.Serialization;
using Common;

namespace BitSoccerWeb.Temp
{
    public class SerializableVector
    {
        [XmlAttribute]
        public int X { get; set; }

        [XmlAttribute]
        public int Y { get; set; }

        public static SerializableVector FromVector(Vector vector)
        {
            return new SerializableVector()
            {
                X = (int) vector.X,
                Y = (int) vector.Y
            };
        }
    }
}