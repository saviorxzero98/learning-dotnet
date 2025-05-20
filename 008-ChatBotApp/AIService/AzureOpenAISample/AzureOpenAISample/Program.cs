using Azure.AI.OpenAI;
using Microsoft.Extensions.Configuration;
using OpenAI.Chat;
using System.ClientModel;

namespace AzureOpenAISample
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            var configuration = GetConfiguration();

            var endpointUrl = configuration.GetValue("AzureOpenAI:Url", string.Empty);
            var deploymentName = configuration.GetValue("AzureOpenAI:ModelName", string.Empty);
            var apiKey = configuration.GetValue("AzureOpenAI:ApiKey", string.Empty);

            if (!string.IsNullOrEmpty(endpointUrl) &&
                !string.IsNullOrEmpty(deploymentName) &&
                !string.IsNullOrEmpty(apiKey))
            {
                await DemoChatMessageAsync(deploymentName, endpointUrl, apiKey);
                //await DemoChatStreamAsync(deploymentName, endpointUrl, apiKey);
            }
        }

        static async Task DemoChatMessageAsync(string deploymentName, string endpointUrl, string apiKey)
        {
            var client = new AzureOpenAIClient(new Uri(endpointUrl), new ApiKeyCredential(apiKey));
            var chatClient = client.GetChatClient(deploymentName);

            var messages = new List<ChatMessage>();

            while (true)
            {
                Console.WriteLine("Say something to Azure OpenAI please!");
                var line = Console.ReadLine();

                if (line == "exit")
                {
                    break;
                }

                messages.Add(new UserChatMessage(line));

                var response = await chatClient.CompleteChatAsync(messages);
                var chatResponse = response.Value.Content.Last().Text;

                Console.WriteLine(chatResponse);
                messages.Add(new AssistantChatMessage(chatResponse));

                Console.WriteLine("\n\n");
            }
        }

        static async Task DemoChatStreamAsync(string deploymentName, string endpointUrl, string apiKey)
        {
            var client = new AzureOpenAIClient(new Uri(endpointUrl), new ApiKeyCredential(apiKey));
            var chatClient = client.GetChatClient(deploymentName);

            var messages = new List<ChatMessage>();

            while (true)
            {
                Console.WriteLine("Say something to Azure OpenAI please!");
                var line = Console.ReadLine();

                if (line == "exit")
                {
                    break;
                }

                messages.Add(new UserChatMessage(line));

                var completionUpdates = chatClient.CompleteChatStreamingAsync(messages);

                await foreach (StreamingChatCompletionUpdate completionUpdate in completionUpdates)
                {
                    if (completionUpdate.ContentUpdate.Count > 0)
                    {
                        foreach (var message in completionUpdate.ContentUpdate)
                        {
                            Console.Write($"{message.Text}");
                        }
                    }
                }

                Console.WriteLine("\n\n");
            }
        }
    

        static IConfiguration GetConfiguration()
        {
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                                                          .AddJsonFile("appsettings.development.json", optional: true, reloadOnChange: true)
                                                          .Build();
            return configuration;
        }
    }
}
