using GuidGeneratorSample.AbpGuids;
using GuidGeneratorSample.UuidExGuids;

namespace GuidGeneratorSample
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // System GUID
            DemoSystemGuid();

            // ABP Framework Guid Generator
            DemoAbpGuidGenerator(SequentialGuidType.SequentialAsString);
            DemoAbpGuidGenerator(SequentialGuidType.SequentialAsBinary);
            DemoAbpGuidGenerator(SequentialGuidType.SequentialAtEnd);
            
            // UUIDNext
            DemoUUIDNextV5("https://google.com.tw", "https://github.com", "https://bing.com");
            DemoUUIDNextV6();
            DemoUUIDNextV7();
            DemoUUIDNextV8();
        }


        static void DemoSystemGuid(int count = 6)
        {
            Console.WriteLine("==============================");
            Console.WriteLine("Guid.NewGuid()");
            Console.WriteLine("==============================");

            for (int i = 0; i < count; i++)
            {
                Guid guid = Guid.NewGuid();
                Console.WriteLine(guid);
            }

            Console.WriteLine("\n");
        }

        static void DemoAbpGuidGenerator(SequentialGuidType type, int count = 6)
        {
            Console.WriteLine("==============================");
            Console.WriteLine($"ABP Framework SequentialGuidGenerator ({type.ToString()})");
            Console.WriteLine("==============================");

            var guidGenerator = new SequentialGuidGenerator(type);
            for (int i = 0; i < count; i++)
            {
                Guid guid = guidGenerator.Create();
                Console.WriteLine(guid);
            }

            Console.WriteLine("\n");
        }

        static void DemoUUIDNextV5(params string[] urls)
        {
            if (urls == null || urls.Length == 0)
            {
                return;
            }

            Console.WriteLine("==============================");
            Console.WriteLine($"UUIDNext UuidV5Generator");
            Console.WriteLine("==============================");

            var guidGenerator = new GuidV5Generator();
            foreach (var url in urls)
            {
                Guid guid = guidGenerator.Create(GuidNamespaceType.Url, url);
                Console.WriteLine(guid);
            }

            Console.WriteLine("\n");
        }

        static void DemoUUIDNextV6(int count = 6)
        {
            Console.WriteLine("==============================");
            Console.WriteLine($"UUIDNext UuidV6Generator");
            Console.WriteLine("==============================");

            var guidGenerator = new GuidV6Generator();
            for (int i = 0; i < count; i++)
            {
                Guid guid = guidGenerator.Create();
                Console.WriteLine(guid);
            }

            Console.WriteLine("\n");
        }

        static void DemoUUIDNextV7(int count = 6)
        {
            Console.WriteLine("==============================");
            Console.WriteLine($"UUIDNext UuidV7Generator");
            Console.WriteLine("==============================");

            var guidGenerator = new GuidV7Generator();
            for (int i = 0; i < count; i++)
            {
                Guid guid = guidGenerator.Create();
                Console.WriteLine(guid);
            }

            Console.WriteLine("\n");
        }

        static void DemoUUIDNextV8(int count = 6)
        {
            Console.WriteLine("==============================");
            Console.WriteLine($"UUIDNext UuidV8Generator");
            Console.WriteLine("==============================");

            var guidGenerator = new GuidV8Generator();
            for (int i = 0; i < count; i++)
            {
                Guid guid = guidGenerator.Create();
                Console.WriteLine(guid);
            }

            Console.WriteLine("\n");
        }
    }
}