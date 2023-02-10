using JUST;
using System.IO;

namespace JustNetSample
{
    class Program
    {
        static void Main(string[] args)
        {
            //read input from JSON file
            string input = File.ReadAllText("InputData/Data.json");

            //read the transformer from a JSON file
            string transformer = File.ReadAllText("TamplateCards/Card.json");

            // do the actual transformation
            string transformedString = JsonTransformer.Transform(transformer, input);

            File.WriteAllText("AdaptiveCards/Card.json", transformedString);
        }
    }
}
