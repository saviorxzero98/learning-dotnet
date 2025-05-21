using Azure.AI.OpenAI;
using Microsoft.Extensions.Configuration;
using OpenAI.Chat;
using System.ClientModel;

namespace AzureOpenAISample.Samples
{
    public abstract class DemoSampleBase
    {
        private readonly IConfiguration _configuration;

        public DemoSampleBase(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task RunAsync()
        {
            if (TryGetChatClient(out var chatClient))
            {
                await HandleAsync(chatClient);
            }
            else
            {
                Console.WriteLine("錯誤，尚未設定 Azure Open API");
            }
        }

        protected abstract Task HandleAsync(ChatClient chatClient);


        protected bool TryGetChatClient(out ChatClient chatClient)
        {
            var endpointUrl = _configuration.GetValue("AzureOpenAI:Url", string.Empty);
            var deploymentName = _configuration.GetValue("AzureOpenAI:ModelName", string.Empty);
            var apiKey = _configuration.GetValue("AzureOpenAI:ApiKey", string.Empty);

            if (!string.IsNullOrEmpty(endpointUrl) &&
                !string.IsNullOrEmpty(deploymentName) &&
                !string.IsNullOrEmpty(apiKey))
            {
                var client = new AzureOpenAIClient(new Uri(endpointUrl), new ApiKeyCredential(apiKey));
                chatClient = client.GetChatClient(deploymentName);
                return true;
            }

            chatClient = null;
            return false;
        }
    }
}
