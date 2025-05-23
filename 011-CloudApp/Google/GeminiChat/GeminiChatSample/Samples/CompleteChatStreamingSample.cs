using GenerativeAI;
using GenerativeAI.Types;
using Microsoft.Extensions.Configuration;

namespace GeminiChatSample.Samples
{
    public class CompleteChatStreamingSample : DemoSampleBase
    {
        public CompleteChatStreamingSample(IConfiguration configuration) : base(configuration)
        {
        }

        protected override async Task HandleAsync(GenerativeModel chatModel)
        {
            var messages = new List<Content>();

            while (true)
            {
                Console.WriteLine("請輸入您的問題：");
                var line = Console.ReadLine();

                if (line == "exit")
                {
                    break;
                }

                messages.Add(new Content(line!, ChatMessageRole.User));

                var completionUpdates = chatModel.StreamContentAsync(new GenerateContentRequest(messages));

                var assistantMessage = string.Empty;

                await foreach (var completionUpdate in completionUpdates)
                {
                    assistantMessage += completionUpdate.Text;
                    Console.Write(completionUpdate.Text);
                }

                messages.Add(new Content(assistantMessage!, ChatMessageRole.Assistant));

                Console.WriteLine("\n\n");
            }
        }
    }
}
