namespace ServerSentEventClient
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            await GetSSEAsync();
            await PostSSEAsync();
        }

        static async Task GetSSEAsync()
        {
            Console.WriteLine("===== Demo Get Server Sent Events =====");

            string url = "https://localhost:7051/stream";

            using var client = new HttpClient();

            using var responseStream = await client.GetStreamAsync(url);
            using var reader = new StreamReader(responseStream);

            while (!reader.EndOfStream)
            {
                var line = await reader.ReadLineAsync();

                if (!string.IsNullOrWhiteSpace(line))
                {
                    Console.WriteLine(line);
                }
            }

            Console.WriteLine("\n\n");
        }

        static async Task PostSSEAsync()
        {
            Console.WriteLine("===== Demo Post Server Sent Events =====");

            string url = "https://localhost:7051/stream";

            using var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, url);
            request.Headers.Add("Accept", "text/event-stream");

            using var response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);

            response.EnsureSuccessStatusCode();

            using var responseStream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
            using var reader = new StreamReader(responseStream);

            while (!reader.EndOfStream)
            {
                var line = await reader.ReadLineAsync();

                if (!string.IsNullOrWhiteSpace(line))
                {
                    Console.WriteLine(line);
                }
            }

            Console.WriteLine("\n\n");
        }
    }
}
