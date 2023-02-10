using RestSharp;
using RestSharp.Serialization.Xml;
using RestSharp.Serializers.NewtonsoftJson;
using RestSharpSample.Models;
using System.Collections.Generic;

namespace RestSharpSample
{
    class Program
    {
        static void Main(string[] args)
        {
            // Demo HTTP GET Json
            DemoHttpGetJson();

            // Demo HTTP GET Xml
            DemoHttpGetXml();
        }

        /// <summary>
        /// Demo HTTP GET Json
        /// </summary>
        static void DemoHttpGetJson()
        {
            // Client
            var client = new RestClient("http://localhost:10446/api/account/json");

            // Headers
            var headers = new Dictionary<string, string>()
            {
                { "Authorization", "ABC"  }
            };
            client.AddDefaultHeaders(headers);

            // JSON Serializer
            client.UseNewtonsoftJson();

            // http://localhost:10446/api/account/json?name=abc&id=123
            var request = new RestRequest();
            request.AddParameter("name", "abc");
            request.AddParameter("id", "123");

            var response = client.Get<AccountProfile>(request);
            AccountProfile result = response.Data;
        }

        /// <summary>
        /// Demo HTTP GET XML
        /// </summary>
        static void DemoHttpGetXml()
        {
            // Client
            var client = new RestClient("http://localhost:10446/api/account/xml");

            // Headers
            var headers = new Dictionary<string, string>()
            {
                { "Authorization", "ABC"  }
            };
            client.AddDefaultHeaders(headers);
            client.UseDotNetXmlSerializer();

            var request = new RestRequest();
            var resp = client.Get(request);
            var response = client.Get<AccountProfile>(request);
        }
    }
}
