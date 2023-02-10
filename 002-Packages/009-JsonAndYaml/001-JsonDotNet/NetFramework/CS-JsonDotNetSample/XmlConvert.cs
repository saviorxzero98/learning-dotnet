using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace CS_JsonDotNetSample
{
    public class XmlConvert
    {
        public static string SerializeObject(object value)
        {
            XmlSerializer ser = new XmlSerializer(value.GetType());
            StringBuilder sb = new StringBuilder();
            StringWriter writer = new StringWriter(sb);
            ser.Serialize(writer, value);
            return sb.ToString();
        }

        public static T DeserializeObject<T>(string s)
        {
            XmlDocument xdoc = new XmlDocument();

            try
            {
                xdoc.LoadXml(s);
                XmlNodeReader reader = new XmlNodeReader(xdoc.DocumentElement);
                XmlSerializer ser = new XmlSerializer(typeof(T));
                object obj = ser.Deserialize(reader);

                return (T)obj;
            }
            catch
            {
                return default(T);
            }
        }
    }
}
