using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;




namespace TrualityEngine.Core
{
    public class XmlSerializer<T>
    {



        public static string GetPath(string fileName)
        {
            return System.IO.Path.Combine(GetFolder(), fileName);
        }

        public static string GetFolder()
        {
            return System.IO.Path.Combine(Directory.GetCurrentDirectory(), "Data");
        }

        public static void SerializeXml(T item, string fileName)
        {
            Directory.CreateDirectory(GetFolder());
            var serializer = new XmlSerializer(typeof(T));

            using StreamWriter writer = new StreamWriter(GetPath($"{fileName}.xml"));
           
            serializer.Serialize(writer, item);
        }

        public static T LoadXml(string fileName)
        {
            var serializer = new XmlSerializer(typeof(T));
            using StreamReader reader = new StreamReader(GetPath($"{fileName}.xml"));
            return (T)serializer.Deserialize(reader);
        }
    }
}



