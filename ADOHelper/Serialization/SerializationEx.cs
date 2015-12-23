using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Json;
using System.Xml.Serialization;
using System.IO;

namespace ADOHelper.Common.Serialization
{
    public static class SerializationEx
    {
        public static string ToJson<T>(this T t) where T : ISerialization
        {
            string json;
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
            using (MemoryStream ms = new MemoryStream())
            {
                serializer.WriteObject(ms, t);
                json = Encoding.UTF8.GetString(ms.ToArray());
            }
            return json;
        }

        public static T FromJson<T>(this string json) where T : ISerialization
        {
            T t;
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
            using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(json)))
            {
                t = (T)serializer.ReadObject(ms);
            }
            return t;
        }

        public static string ToXml<T>(this T t) where T : ISerialization
        {
            string xml;
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (MemoryStream ms = new MemoryStream())
            {
                serializer.Serialize(ms, t);
                xml = Encoding.UTF8.GetString(ms.ToArray());
            }
            return xml;
        }

        public static T FromXml<T>(this string xml) where T : ISerialization
        {
            T t;
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(xml)))
            {
                t = (T)serializer.Deserialize(ms);
            }
            return t;
        }
    }
}
