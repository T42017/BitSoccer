using System.Xml.Serialization;
using Common;

namespace bitsoccer.net.Data
{
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
}