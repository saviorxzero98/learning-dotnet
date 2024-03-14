using Newtonsoft.Json.Linq;

namespace NewtonsoftJsonSample.Extensions
{
    public static class JTokenExtensions
    {
        /// <summary>
        /// 檢查 JToken 是否為空
        /// </summary>
        /// <param name="jToken"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this JToken jToken)
        {
            switch (jToken.Type)
            {
                case JTokenType.String:
                    string text = jToken.ToString();
                    return string.IsNullOrEmpty(text);

                case JTokenType.Array:
                    return (((JArray)jToken).Count == 0);

                case JTokenType.Object:
                    return (((JObject)jToken).Count == 0);

                case JTokenType.Undefined:
                case JTokenType.Null:
                case JTokenType.None:
                    return true;

            }
            return false;
        }

        /// <summary>
        /// Merge JToken
        /// </summary>
        /// <param name="jToken"></param>
        /// <param name="appendData"></param>
        /// <returns></returns>
        public static JToken Merge(this JToken jToken, JToken appendData)
        {
            if (jToken is JObject || jToken is JArray)
            {
                // Merge
                if (appendData is JObject)
                {
                    return jToken.MergeObject(appendData as JObject);
                }
                else if (appendData is JArray)
                {
                    return jToken.MergeArray(appendData as JArray);
                }
                else
                {
                    return jToken;
                }
            }
            return jToken;
        }

        /// <summary>
        /// Merge JObject
        /// </summary>
        /// <param name="jToken"></param>
        /// <param name="appendData"></param>
        /// <returns></returns>
        public static JToken MergeObject(this JToken jToken, JObject appendData)
        {
            try
            {
                JToken newJToken = jToken.DeepClone();

                if (newJToken is JObject)
                {
                    JObject jObject = newJToken as JObject;

                    jObject.Merge(appendData, new JsonMergeSettings()
                    {
                        MergeArrayHandling = MergeArrayHandling.Union,
                        MergeNullValueHandling = MergeNullValueHandling.Ignore
                    });
                    return newJToken;
                }

                if (newJToken is JArray)
                {
                    JArray jArray = (newJToken as JArray);
                    jArray.Add(appendData);

                    return newJToken;
                }
            }
            catch
            {

            }
            return jToken;
        }

        /// <summary>
        /// Merge JArray
        /// </summary>
        /// <param name="jToken"></param>
        /// <param name="appendData"></param>
        /// <returns></returns>
        public static JToken MergeArray(this JToken jToken, JArray appendData)
        {
            try
            {
                var newJToken = jToken.DeepClone();

                if (newJToken is JArray)
                {
                    JArray jArray = (newJToken as JArray);

                    jArray.Merge(appendData, new JsonMergeSettings()
                    {
                        MergeArrayHandling = MergeArrayHandling.Union,
                        MergeNullValueHandling = MergeNullValueHandling.Ignore
                    });
                    return newJToken;
                }
            }
            catch
            {

            }
            return jToken;
        }
    }
}
