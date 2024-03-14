using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Text;
using System.Xml;

namespace HttpApiClientToolkit.Toolkits
{
    public class XmlConverter
    {
        /// <summary>
        /// XML Serialize
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="settings"></param>
        /// <returns></returns>
        public static string SerializeObject<T>(T value, XmlWriterSettings settings = null)
        {
            if (value == null)
            {
                return string.Empty;
            }

            string rootNodeName = typeof(T).Name;
            return SerializeObject(value, rootNodeName, settings);
        }
        /// <summary>
        /// XML Serialize
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="rootNodeName"></param>
        /// <param name="settings"></param>
        /// <returns></returns>
        public static string SerializeObject<T>(T value, string rootNodeName, XmlWriterSettings settings = null)
        {
            if (value == null)
            {
                return string.Empty;
            }

            JToken json = JToken.FromObject(value);
            XmlDocument xml = JsonConvert.DeserializeXmlNode(json.ToString(), rootNodeName);

            if (settings == null)
            {
                settings = new XmlWriterSettings()
                {
                    Encoding = Encoding.UTF8
                };
            }

            using (var ms = new MemoryStream())
            {
                using (var writer = XmlWriter.Create(ms, settings))
                {
                    xml.WriteTo(writer);
                    writer.Flush();
                    var xmlString = settings.Encoding.GetString(ms.ToArray());
                    return xmlString;
                }
            }
        }


        /// <summary>
        /// XML Deserialize
        /// </summary>
        /// <param name="xmlString"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static JToken DeserializeObject(string xmlString, Encoding encoding = null)
        {
            if (string.IsNullOrEmpty(xmlString))
            {
                return default(JToken);
            }

            if (encoding == null)
            {
                encoding = Encoding.UTF8;
            }

            using (var ms = new MemoryStream(encoding.GetBytes(xmlString)))
            {
                XmlDocument xml = new XmlDocument();
                xml.Load(ms);

                string jsonText = JsonConvert.SerializeXmlNode(xml.DocumentElement);
                JToken json = JToken.Parse(jsonText);
                return json;
            }
        }
        /// <summary>
        /// XML Deserialize
        /// </summary>
        /// <param name="xmlString"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static JToken DeserializeObject(string xmlString, string rootNodeName, Encoding encoding = null)
        {
            if (string.IsNullOrEmpty(xmlString))
            {
                return default(JToken);
            }

            if (encoding == null)
            {
                encoding = Encoding.UTF8;
            }

            using (var ms = new MemoryStream(encoding.GetBytes(xmlString)))
            {
                XmlDocument xml = new XmlDocument();
                xml.Load(ms);

                string jsonText = JsonConvert.SerializeXmlNode(xml.DocumentElement);
                JToken json = JToken.Parse(jsonText);

                if (json != null && !string.IsNullOrEmpty(rootNodeName))
                {
                    return json[rootNodeName];
                }

                return json;
            }
        }


        /// <summary>
        /// XML Deserialize
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xmlString"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static T DeserializeObject<T>(string xmlString, Encoding encoding = null)
        {
            if (encoding == null)
            {
                encoding = Encoding.UTF8;
            }

            using (var ms = new MemoryStream(encoding.GetBytes(xmlString)))
            {
                XmlDocument xml = new XmlDocument();
                xml.Load(ms);

                string jsonText = JsonConvert.SerializeXmlNode(xml.DocumentElement);
                JToken json = JToken.Parse(jsonText);
                string rootNodeName = typeof(T).Name;
                return json[rootNodeName].ToObject<T>();
            }
        }
        /// <summary>
        /// XML Deserialize
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xmlString"></param>
        /// <param name="rootNodeName"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static T DeserializeObject<T>(string xmlString, string rootNodeName, Encoding encoding = null)
        {
            if (encoding == null)
            {
                encoding = Encoding.UTF8;
            }

            using (var ms = new MemoryStream(encoding.GetBytes(xmlString)))
            {
                XmlDocument xml = new XmlDocument();
                xml.Load(ms);

                string jsonText = JsonConvert.SerializeXmlNode(xml.DocumentElement);
                JToken json = JToken.Parse(jsonText);
                return json[rootNodeName].ToObject<T>();
            }
        }
    }
}
