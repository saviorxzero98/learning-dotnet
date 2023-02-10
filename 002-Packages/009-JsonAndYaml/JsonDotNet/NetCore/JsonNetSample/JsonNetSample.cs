using System.Text.Json;

namespace JsonNetSample
{
    public class JsonNetSample
    {
        public string JsonSerialize<T>(T obj)
        {
            return JsonSerializer.Serialize(obj);
        }

        public T JsonDeserialize<T>(string json)
        {
            return JsonSerializer.Deserialize<T>(json);
        }
    }
}
