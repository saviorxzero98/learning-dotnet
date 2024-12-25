using StackExchange.Redis;

namespace RedisSampleClient
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            var connectionString = "localhost:6379";

            using (var redis = ConnectionMultiplexer.Connect(connectionString))
            {
                IDatabase db = redis.GetDatabase();

                const string Key = "TestKey";
                
                // Set
                await db.StringSetAsync(Key, "TestValue");

                // Get
                string? value = await db.StringGetAsync(Key);
                Console.WriteLine(value);
            }
        }
    }
}
