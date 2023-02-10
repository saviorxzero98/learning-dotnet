using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace EvaluateExpressionSample.Scripts
{
    public static class Json
    {
        public static string Create(IEnumerable<string> data)
        {
            return JsonConvert.SerializeObject(data);
        }
        public static string Create(IEnumerable<int> data)
        {
            return JsonConvert.SerializeObject(data);
        }
        public static string Create(IEnumerable<double> data)
        {
            return JsonConvert.SerializeObject(data);
        }
        public static string Create(IEnumerable<bool> data)
        {
            return JsonConvert.SerializeObject(data);
        }

        public static string Create(object data)
        {
            return JObject.FromObject(data).ToString();
        }
    }
}
