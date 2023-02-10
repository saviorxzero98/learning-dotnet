using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.IO.Compression;

namespace CS_SqlKata
{
    public class BotStateStoreDao
    {
        public int Id { get; set; }
        public string BotId { get; set; }
        public string StateKey { get; set; }
        public byte[] Data { get; set; }
        public string TimeStamp { get; set; }
        public DateTime UpdateDate
        {
            get
            {
                DateTime date;
                if (DateTime.TryParse(TimeStamp, out date))
                {
                    return date;
                }
                return new DateTime();
            }
            set
            {
                TimeStamp = value.ToString("yyyy-MM-dd HH:mm:ss.fff");
            }
        }
        public JObject BotData
        {
            get
            {
                return Deserialize(Data);
            }
            set
            {
                if (value != null)
                {
                    Data = Serialize(value);
                }
            }
        }

        private static byte[] Serialize(JObject data)
        {
            using (var cmpStream = new MemoryStream())
            using (var stream = new GZipStream(cmpStream, CompressionMode.Compress))
            using (var streamWriter = new StreamWriter(stream))
            {
                JsonSerializerSettings settings = new JsonSerializerSettings()
                {
                    Formatting = Formatting.None,
                    NullValueHandling = NullValueHandling.Ignore
                };
                string serializedJSon = JsonConvert.SerializeObject(data, settings);
                streamWriter.Write(serializedJSon);
                streamWriter.Close();
                stream.Close();
                return cmpStream.ToArray();
            }
        }
        private static JObject Deserialize(byte[] bytes)
        {
            using (var stream = new MemoryStream(bytes))
            using (var gz = new GZipStream(stream, CompressionMode.Decompress))
            using (var streamReader = new StreamReader(gz))
            {
                return JsonConvert.DeserializeObject<JObject>(streamReader.ReadToEnd());
            }
        }
    }
}
