using System;
using System.IO;
using System.IO.IsolatedStorage;
using System.Xml.Serialization;

namespace BitSoccerWeb.Temp
{
    public static class ObjectXmlSerializer<T> where T : class
    {
        #region Publika medlemmar
        public static T Load(string path)
        {
            T serializableObject = LoadFromDocumentFormat(null, path, null);
            return serializableObject;
        }

        public static void Save(T serializableObject, string path)
        {
            SaveToDocumentFormat(serializableObject, null, path, null);
        }

        public static string Save(T serializableObject)
        {
            return SaveToString(serializableObject, null);
        }

        #endregion

        #region Privata medlemmar
        private static T LoadFromDocumentFormat(System.Type[] extraTypes, string path,
            IsolatedStorageFile isolatedStorageFolder)
        {
            T serializableObject = null;

            using (TextReader textReader = CreateTextReader(isolatedStorageFolder, path))
            {
                XmlSerializer xmlSerializer = CreateXmlSerializer(extraTypes);
                serializableObject = xmlSerializer.Deserialize(textReader) as T;
            }
            return serializableObject;
        }

        private static TextReader CreateTextReader(IsolatedStorageFile isolatedStorageFolder, string path)
        {
            TextReader textReader = null;

            if (isolatedStorageFolder == null)
                textReader = new StreamReader(path);
            else
                textReader = new StreamReader(new IsolatedStorageFileStream(path,
                    FileMode.Open, isolatedStorageFolder));

            return textReader;
        }

        private static void SaveToDocumentFormat(T serializableObject,
            System.Type[] extraTypes, string path, IsolatedStorageFile isolatedStorageFolder)
        {
            using (TextWriter textWriter = CreateTextWriter(isolatedStorageFolder, path))
            {
                XmlSerializer xmlSerializer = CreateXmlSerializer(extraTypes);
                xmlSerializer.Serialize(textWriter, serializableObject);
            }
        }

        private static string SaveToString(T serializableObject,
            System.Type[] extraTypes)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                XmlSerializer xmlSerializer = CreateXmlSerializer(extraTypes);
                xmlSerializer.Serialize(ms, serializableObject);

                ms.Position = 0;
                var sr = new StreamReader(ms);
                return sr.ReadToEnd();
            }
        }

        private static TextWriter CreateTextWriter(IsolatedStorageFile
                            isolatedStorageFolder, string path)
        {
            TextWriter textWriter = null;

            if (isolatedStorageFolder == null)
                textWriter = new StreamWriter(path);
            else
                textWriter = new StreamWriter(new IsolatedStorageFileStream(path,
                                    FileMode.OpenOrCreate, isolatedStorageFolder));

            return textWriter;
        }

        private static XmlSerializer CreateXmlSerializer(System.Type[] extraTypes)
        {
            Type ObjectType = typeof(T);

            XmlSerializer xmlSerializer = null;

            if (extraTypes != null)
                xmlSerializer = new XmlSerializer(ObjectType, extraTypes);
            else
                xmlSerializer = new XmlSerializer(ObjectType);

            return xmlSerializer;
        }
        #endregion
    }
}