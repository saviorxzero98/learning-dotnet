using JsonNetSample.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace JsonNetSample
{
    public class NewtonsoftJsonSample
    {


        public void StartDemo()
        {
            // File Path
            string filePath = "JsonData\\Data.json";

            // Reade File
            string json = ReadJsonFile(filePath);

            // Deserialize JSON
            Node node = JsonConvert.DeserializeObject<Node>(json);

            // Serialize JSON
            string outJsonString = JsonConvert.SerializeObject(node);

            // Print
            Console.WriteLine(outJsonString);
        }


        public string ReadJsonFile(string filePath)
        {
            using (StreamReader r = new StreamReader(filePath))
            {
                string jsonString = r.ReadToEnd();
                return jsonString;
            }
        }


        public void DemoDepthIssus()
        {
            // File Path
            string filePath = "JsonData\\Data.json";

            // Reade File
            string json = ReadJsonFile(filePath);

            var data = GetTestDataLoop(1, 400);

            KnownTypesBinder knownTypesBinder = new KnownTypesBinder
            {
                KnownTypes = new List<Type> { typeof(Node) }
            };
            var serializer = JsonSerializer.Create(new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
                Formatting = Formatting.Indented,
                MaxDepth = null
            });
            JObject jobj = JObject.FromObject(data, serializer);

            var node = jobj.ToObject<Node>(serializer);

            Console.WriteLine(jobj.ToString());
        }


        protected Node GetTestDataLoop(int index, int depth)
        {
            var rootNode = new Node()
            {
                Id = $"Node {index}",
                Children = new List<Node>()
            };

            var currentNode = rootNode;

            for (int i = index + 1; i < depth; i++)
            {
                var childNode = new Node()
                {
                    Id = $"Node {i}",
                    Children = new List<Node>()
                };
                currentNode.Children
                           .Add(childNode);

                currentNode = childNode;
            }

            return rootNode;
        }

        protected Node GetTestDataRc(int index, int depth)
        {
            if (index == depth)
            {
                return new Node()
                {
                    Id = $"Node #{index}",
                    Children = new List<Node>()
                };
            }

            return new Node()
            {
                Id = $"Node #{index}",
                Children = new List<Node>()
                {
                    GetTestDataRc(index + 1, depth)
                }
            };
        }
    }

    public class KnownTypesBinder : ISerializationBinder
    {
        public IList<Type> KnownTypes { get; set; }

        public Type BindToType(string assemblyName, string typeName)
        {
            return KnownTypes.SingleOrDefault(t => t.Name == typeName);
        }

        public void BindToName(Type serializedType, out string assemblyName, out string typeName)
        {
            assemblyName = null;
            typeName = serializedType.Name;
        }
    }
}
