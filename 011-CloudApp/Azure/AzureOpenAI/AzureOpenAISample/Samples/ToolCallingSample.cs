using Microsoft.Extensions.Configuration;
using OpenAI.Chat;

namespace AzureOpenAISample.Samples
{
    public class ToolCallingSample : DemoSampleBase
    {
        public ToolCallingSample(IConfiguration configuration) : base(configuration)
        {
        }

        protected override Task HandleAsync(ChatClient chatClient)
        {
            return Task.CompletedTask;
        }
    }
}
