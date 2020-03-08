using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml;
using System.Xml.Serialization;

namespace DccCharCreator.core
{
    public abstract class Serializer
    {
        public static string BasePath { get; set; } = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        public static Dictionary<int, T> Load<T>(string fileName, Action<Dictionary<int, T>> validate, Func<T, int> index)
        {
            var result = Load<T>(fileName).ToDictionary(index);
            validate(result);
            return result;
        }

        public static T[] Load<T>(string fileName)
        {
            using var xmlReader = XmlReader.Create(Path.Combine(BasePath, "Xml", fileName));
            var xmlSerializer = new XmlSerializer(typeof(T[]));
            xmlSerializer.UnknownAttribute += Log;
            xmlSerializer.UnknownElement += Log;
            xmlSerializer.UnknownNode += Log;
            return (T[])xmlSerializer.Deserialize(xmlReader);
        }

        private static void Log(object sender, XmlNodeEventArgs e)
        {
            //throw new NotImplementedException();
        }

        private static void Log(object sender, XmlElementEventArgs e)
        {
            //throw new NotImplementedException();
        }

        private static void Log(object sender, XmlAttributeEventArgs e)
        {
            //throw new NotImplementedException();
        }

        public static void Save<T>(string fileName, List<T> values)
        {
            var xmlSerializer = new XmlSerializer(values.GetType());
            using var xmlWriter = XmlWriter.Create(Path.Combine(BasePath, "Xml", fileName), new XmlWriterSettings { Indent = true });
            xmlSerializer.Serialize(xmlWriter, values);
        }

    }
}
