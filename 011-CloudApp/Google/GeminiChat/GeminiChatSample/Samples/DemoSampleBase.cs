using GenerativeAI;
using Microsoft.Extensions.Configuration;

namespace GeminiChatSample.Samples
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
            if (TryGetChatModel(out var chetModel))
            {
                await HandleAsync(chetModel);
            }
            else
            {
                Console.WriteLine("錯誤，尚未設定 Google AI API");
            }
        }

        protected abstract Task HandleAsync(GenerativeModel chatClient);

        protected bool TryGetChatModel(out GenerativeModel chetModel)
        {
            var apiKey = _configuration.GetValue("GoogleAi:ApiKey", string.Empty);
            var modelName = _configuration.GetValue("GoogleAi:ModelName", string.Empty);
            

            if (!string.IsNullOrEmpty(modelName) &&
                !string.IsNullOrEmpty(apiKey))
            {
                var googleAi = new GoogleAi(apiKey);
                chetModel = googleAi.CreateGenerativeModel(modelName);
                return true;
            }

            chetModel = null;
            return false;
        }
    }
}
