using System.Text.Json;

namespace JsonSample
{
    public class SystemJsonSample
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
