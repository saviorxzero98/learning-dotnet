using Newtonsoft.Json.Linq;
using NewtonsoftJsonSample.Extensions;

namespace NewtonsoftJsonSample
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DemoMergeJToken1();

            DemoMergeJToken2();
        }

        static void DemoMergeJToken1()
        {
            // Object 1
            var blueJToken = JToken.FromObject(new 
            {
                Name = "KMT",
                Members = new string[]
                {
                    "Tentacool",
                    "Angkor Wat"
                },
                PrimaryColor = "KMT#000095",
                SecondaryColor = "TPP#28C8C8",
                Detial = new
                {
                    Color = "#000095"
                }
            });

            // Object 2
            var whiteJToken = JToken.FromObject(new
            {
                Email = "KP@tpp.com.tw",
                Members = new string[]
                {
                    "33"
                },
                PrimaryColor = "TPP#28C8C8",
                SecondaryColor = "KMT#000095",
                Detial = new
                {
                    Color = "#28C8C8"
                }
            });

            // Merge Object
            var mergeWhiteBlue = blueJToken.Merge(whiteJToken);
            var mergeBlueWhite = whiteJToken.Merge(blueJToken);

            Console.WriteLine($"Object 1:\n{blueJToken}");
            Console.WriteLine($"Object 2:\n{whiteJToken}");
            Console.WriteLine($"Merged Object 1:\n{mergeBlueWhite}");
            Console.WriteLine($"Merged Object 2:\n{mergeWhiteBlue}");
        }


        static void DemoMergeJToken2()
        {
            // Array 1
            var jtoken1 = JToken.FromObject(new string[]
                {
                    "Two",
                    "Three"
                });

            // Array 2
            var jtoken2 = JToken.FromObject(new string[]
                {
                    "Four",
                    "Three"
                });

            // Merge Object
            var mergeJToken = jtoken1.Merge(jtoken2);

            Console.WriteLine($"Object 1:\n{jtoken1}");
            Console.WriteLine($"Object 2:\n{jtoken2}");
            Console.WriteLine($"Merged Object\n{mergeJToken}");
        }
    }
}