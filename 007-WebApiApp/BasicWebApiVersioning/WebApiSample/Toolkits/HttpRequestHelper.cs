using Microsoft.AspNetCore.Http;
using System.Linq;

namespace WebApiSample.Toolkits
{
    public static class HttpRequestHelper
    {
        public static (bool result, string value) GetHeader(HttpRequest request, string key)
        {
            if (request.Headers.ContainsKey(key))
            {
                var value = request.Headers[key].FirstOrDefault();
                return (result: true, value: value);
            }
            else
            {
                return (result: true, value: string.Empty);
            }
        }
    }
}
