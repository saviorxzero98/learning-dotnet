using Azure;
using Azure.AI.Inference;
using Microsoft.Extensions.Configuration;

namespace GithubModelSample.Samples
{
    public abstract class AiInferenceSampleBase
    {
        protected readonly IConfiguration _configuration;
        public const string Endpoint = "https://models.github.ai/inference";

        public AiInferenceSampleBase(IConfiguration configuration)
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

        protected abstract Task HandleAsync(ChatCompletionsClient chatClient);

        protected bool TryGetChatClient(out ChatCompletionsClient chatClient)
        {
            var apiKey = _configuration.GetValue("Github:ApiKey", string.Empty);

            if (!string.IsNullOrEmpty(apiKey))
            {
                chatClient = new ChatCompletionsClient(new Uri(Endpoint), new AzureKeyCredential(apiKey));
                return true;
            }

            chatClient = null;
            return false;
        }

        protected string GetModelName()
        {
            return _configuration.GetValue("Github:ModelName", string.Empty)!;
        }
    }
}
