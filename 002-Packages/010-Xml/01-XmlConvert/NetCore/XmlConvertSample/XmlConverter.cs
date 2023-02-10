using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace XmlConvertSample
{
    public static class XmlConverter
    {
        #region XML 序列化和反序列化處理

        /// <summary>
        /// XML 序列化
        /// </summary>
        /// <param name="value"></param>
        /// <param name="rootNodeName"></param>
        /// <param name="encoding"></param>
        /// <param name="indent"></param>
        /// <returns></returns>
        public static string SerializeObject(JToken value, string rootNodeName, Encoding encoding = null, bool indent = false)
        {
            // 初始化 XML 設定
            var settings = new XmlWriterSettings()
            {
                Encoding = encoding ?? Encoding.UTF8,
                Indent = indent
            };

            // 處理 XML 序列化
            return SerializeObject(value, rootNodeName, settings);
        }
        /// <summary>
        /// XML 序列化
        /// </summary>
        /// <param name="value"></param>
        /// <param name="rootNodeName"></param>
        /// <param name="settings"></param>
        /// <returns></returns>
        public static string SerializeObject(JToken value, string rootNodeName, XmlWriterSettings settings)
        {
            // JSON 轉換成 XML
            XmlDocument xml = JsonConvert.DeserializeXmlNode(value.ToString(), rootNodeName);

            // 初始化 XML 設定
            if (settings == null)
            {
                settings = new XmlWriterSettings()
                {
                    Encoding = Encoding.UTF8
                };
            }

            // 處理 XML 序列化
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
        /// XML 序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="encoding"></param>
        /// <param name="indent"></param>
        /// <returns></returns>
        public static string SerializeObject<T>(T value, Encoding encoding = null, bool indent = false)
        {
            /// 初始化 XML 設定
            var settings = new XmlWriterSettings()
            {
                Encoding = encoding ?? Encoding.UTF8,
                Indent = indent
            };

            // 處理  XML 序列化
            return SerializeObject(value, settings);
        }
        /// <summary>
        /// XML 序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="settings"></param>
        /// <returns></returns>
        public static string SerializeObject<T>(T value, XmlWriterSettings settings)
        {
            // 取得物件名稱作為 XML Root Node 名稱
            string rootNodeName = typeof(T).Name;

            // 處理  XML 序列化
            return SerializeObject(value, rootNodeName, settings);
        }
        /// <summary>
        /// XML 序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="rootNodeName"></param>
        /// <param name="encoding"></param>
        /// <param name="indent"></param>
        /// <returns></returns>
        public static string SerializeObject<T>(T value, string rootNodeName, Encoding encoding = null, bool indent = false)
        {
            /// 初始化 XML 設定
            var settings = new XmlWriterSettings()
            {
                Encoding = encoding ?? Encoding.UTF8,
                Indent = indent
            };

            // 處理 XML 序列化
            return SerializeObject(value, rootNodeName, settings);
        }
        /// <summary>
        /// XML 序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="rootNodeName"></param>
        /// <param name="settings"></param>
        /// <returns></returns>
        public static string SerializeObject<T>(T value, string rootNodeName, XmlWriterSettings settings)
        {
            // 檢查 Value
            if (value == null)
            {
                return string.Empty;
            }

            // 轉換成 JToken
            JToken json = JToken.FromObject(value);
            string jsonString = json.ToString();

            // JSON 轉換成 XML
            XmlDocument xml = JsonConvert.DeserializeXmlNode(jsonString, rootNodeName);

            // 初始化 XML 設定
            if (settings == null)
            {
                settings = new XmlWriterSettings()
                {
                    Encoding = Encoding.UTF8
                };
            }

            // 處理 XML 序列化
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
        /// XML 反序列化
        /// </summary>
        /// <param name="xmlString"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static JToken DeserializeObject(string xmlString, Encoding encoding = null)
        {
            return DeserializeObject(xmlString, null, encoding);
        }
        /// <summary>
        /// XML 反序列化
        /// </summary>
        /// <param name="xmlString"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static JToken DeserializeObject(string xmlString, string rootNodeName, Encoding encoding = null)
        {
            // 檢查 XML 字串
            if (string.IsNullOrEmpty(xmlString))
            {
                return default(JToken);
            }

            // 設定編碼
            if (encoding == null)
            {
                encoding = Encoding.UTF8;
            }

            // 處理轉換
            using (var ms = new MemoryStream(encoding.GetBytes(xmlString)))
            {
                // 讀取 XML
                XmlDocument xml = new XmlDocument();
                xml.Load(ms);

                // 轉成 JSON
                string jsonText = JsonConvert.SerializeXmlNode(xml.DocumentElement);
                JToken json = JToken.Parse(jsonText);

                // 是否指定 Root Node Name
                if (json != null && !string.IsNullOrEmpty(rootNodeName))
                {
                    return json[rootNodeName];
                }
                return json;
            }
        }
        
        /// <summary>
        /// XML 反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xmlString"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static T DeserializeObject<T>(string xmlString, Encoding encoding = null)
        {
            // 取得物件名稱作為 XML Root Node 名稱
            string rootNodeName = typeof(T).Name;

            // 處理 XML 反序列化
            return DeserializeObject<T>(xmlString, rootNodeName, encoding);
        }
        /// <summary>
        /// XML 反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xmlString"></param>
        /// <param name="rootNodeName"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static T DeserializeObject<T>(string xmlString, string rootNodeName, Encoding encoding = null)
        {
            // 檢查 XML 字串
            if (string.IsNullOrEmpty(xmlString))
            {
                return default(T);
            }

            // 設定編碼
            if (encoding == null)
            {
                encoding = Encoding.UTF8;
            }

            // 處理轉換
            using (var ms = new MemoryStream(encoding.GetBytes(xmlString)))
            {
                // 讀取 XML
                XmlDocument xml = new XmlDocument();
                xml.Load(ms);

                // 轉成 JSON
                string jsonText = JsonConvert.SerializeXmlNode(xml.DocumentElement);
                JToken json = JToken.Parse(jsonText);

                // 轉成指定物件
                if (json != null &&
                    !string.IsNullOrEmpty(rootNodeName) &&
                    json[rootNodeName] != null)
                {
                    return json[rootNodeName].ToObject<T>();
                }
                return default(T);
            }
        }

        #endregion


        #region XML 與 JSON 轉換

        /// <summary>
        /// JSON String 轉 XML String
        /// </summary>
        /// <param name="jsonString"></param>
        /// <param name="rootNodeName"></param>
        /// <param name="encoding"></param>
        /// <param name="indent"></param>
        /// <returns></returns>
        public static string FromJsonString(string jsonString, string rootNodeName, Encoding encoding = null, bool indent = false)
        {
            // 初始化 XML 設定
            var settings = new XmlWriterSettings()
            {
                Encoding = encoding ?? Encoding.UTF8,
                Indent = indent
            };

            return FromJsonString(jsonString, rootNodeName, settings);
        }
        /// <summary>
        /// JSON String 轉 XML String
        /// </summary>
        /// <param name="jsonString"></param>
        /// <param name="rootNodeName"></param>
        /// <param name="settings"></param>
        /// <returns></returns>
        public static string FromJsonString(string jsonString, string rootNodeName, XmlWriterSettings settings)
        {
            try
            {
                var value = JToken.Parse(jsonString);
                return SerializeObject(value, rootNodeName, settings);
            }
            catch
            {
                return string.Empty;
            }
        }


        /// <summary>
        /// XML String 轉 JSON String
        /// </summary>
        /// <param name="xmlString"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static JToken ToJsonString(string xmlString, Encoding encoding = null)
        {
            JToken json = DeserializeObject(xmlString, encoding);

            if (json != null)
            {
                string jsonString = json.ToString();
                return jsonString;
            }
            return string.Empty;
        }
        /// <summary>
        /// XML String 轉 JSON String
        /// </summary>
        /// <param name="xmlString"></param>
        /// <param name="rootNodeName"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static JToken ToJsonString(string xmlString, string rootNodeName = null, Encoding encoding = null)
        {
            JToken json = DeserializeObject(xmlString, rootNodeName, encoding);

            if (json != null)
            {
                string jsonString = json.ToString();
                return jsonString;
            }
            return string.Empty;
        }

        #endregion


        #region XML 序列化和反序列化處理 (僅限 Class 物件)

        /// <summary>
        /// XML 序列化 (僅限 Class 物件)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="encoding"></param>
        /// <param name="indent"></param>
        /// <returns></returns>
        public static string Serialize<T>(T value, Encoding encoding = null, bool indent = false) where T : class
        {
            // 初始化 XML 設定
            var settings = new XmlWriterSettings()
            {
                Encoding = encoding ?? Encoding.UTF8,
                Indent = indent
            };

            // 處理 XML 序列化
            return Serialize(value, settings);
        }
        /// <summary>
        /// XML 序列化 (僅限 Class 物件)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="settings"></param>
        /// <returns></returns>
        public static string Serialize<T>(T value, XmlWriterSettings settings) where T : class
        {
            // 建立 XmlSerializer
            XmlSerializer serializer = new XmlSerializer(value.GetType());

            // 初始化 XML 設定
            if (settings == null)
            {
                settings = new XmlWriterSettings()
                {
                    Encoding = Encoding.UTF8
                };
            }

            // 處理 XML 序列化
            using (var ms = new MemoryStream())
            {
                using (var writer = XmlWriter.Create(ms, settings))
                {
                    serializer.Serialize(writer, value);
                    string xmlString = settings.Encoding.GetString(ms.ToArray());
                    return xmlString;
                }
            }
        }


        /// <summary>
        /// XML 反序列化 (僅限 Class 物件)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xmlString"></param>
        /// <returns></returns>
        public static T Deserialize<T>(string xmlString, Encoding encoding = null) where T : class
        {
            // 建立 XmlSerializer
            XmlSerializer deserializer = new XmlSerializer(typeof(T));

            // 設定編碼
            if (encoding == null)
            {
                encoding = Encoding.UTF8;
            }

            // 處理 XML 反序列化
            using (var ms = new MemoryStream(encoding.GetBytes(xmlString)))
            {
                object deserializationObj = deserializer.Deserialize(ms);
                return deserializationObj as T;
            }
        }

        #endregion
    }
}
