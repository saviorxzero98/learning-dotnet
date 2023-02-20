using YamlDotNet.Serialization;
using YamlParserSample.Models;

namespace YamlParserSample
{
    public class Program
    {
        static void Main(string[] args)
        {
            DemoSample1("Data/Organization.yaml");
        }

        static void DemoSample1(string path)
        {
            // Read File
            string content = File.ReadAllText(path);

            // Deserialize
            var deserializer = new DeserializerBuilder().Build();
            var result = deserializer.Deserialize<Organization>(content);

            // Serialize
            var serializer = new SerializerBuilder().Build();
            var yaml = serializer.Serialize(result);

            Console.WriteLine(yaml);
        }
    }
}