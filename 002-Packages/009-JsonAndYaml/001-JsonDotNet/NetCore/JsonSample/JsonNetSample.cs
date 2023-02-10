using Newtonsoft.Json;

namespace JsonSample
{
    public class JsonNetSample
    {
        public string JsonSerialize<T>(T obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        public T JsonDeserialize<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
