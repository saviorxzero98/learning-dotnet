using Microsoft.Extensions.Configuration;
using OpenAI;
using OpenAI.Chat;
using System.ClientModel;

namespace GithubModelSample.Samples
{
    public abstract class OpenAiSampleBase
    {
        protected readonly IConfiguration _configuration;
        public const string Endpoint = "https://models.github.ai/inference";

        public OpenAiSampleBase(IConfiguration configuration)
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
                Console.WriteLine("錯誤，尚未設定 Github Access Token");
            }
        }

        protected abstract Task HandleAsync(ChatClient chatClient);

        protected bool TryGetChatClient(out ChatClient chatClient)
        {
            var apiKey = _configuration.GetValue("Github:ApiKey", string.Empty);
            var modelName = _configuration.GetValue("Github:ModelName", string.Empty);

            if (!string.IsNullOrEmpty(modelName) &&
                !string.IsNullOrEmpty(apiKey))
            {
                var openAIOptions = new OpenAIClientOptions()
                {
                    Endpoint = new Uri(Endpoint)
                };
                chatClient = new ChatClient(modelName, new ApiKeyCredential(apiKey), openAIOptions);
                return true;
            }

            chatClient = null;
            return false;
        }
    }
}
