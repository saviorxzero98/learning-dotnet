using Microsoft.Extensions.Configuration;
using OpenAI.Chat;
using System.ClientModel;

namespace OpenAIChatSample.Samples
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
                Console.WriteLine("錯誤，尚未設定 Open API");
            }
        }

        protected abstract Task HandleAsync(ChatClient chatClient);

        protected bool TryGetChatClient(out ChatClient chatClient)
        {
            var apiKey = _configuration.GetValue("OpenAI:ApiKey", string.Empty);
            var modelName = _configuration.GetValue("OpenAI:ModelName", string.Empty);
            
            if (!string.IsNullOrEmpty(modelName) &&
                !string.IsNullOrEmpty(apiKey))
            {
                chatClient = new ChatClient(modelName, new ApiKeyCredential(apiKey));
                return true;
            }

            chatClient = null;
            return false;
        }
    }
}
