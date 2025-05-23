using GenerativeAI;
using GenerativeAI.Types;
using Microsoft.Extensions.Configuration;

namespace GeminiChatSample.Samples
{
    public class CompleteChatSample : DemoSampleBase
    {
        public CompleteChatSample(IConfiguration configuration) : base(configuration)
        {
        }

        protected override async Task HandleAsync(GenerativeModel chatModel)
        {
            var chatSession = chatModel.StartChat();
            var messages = new List<Content>();

            while (true)
            {
                Console.WriteLine("請輸入您的問題：");
                var line = Console.ReadLine();

                if (line == "exit")
                {
                    break;
                }

                //var response = await chatSession.GenerateContentAsync(line!);

                messages.Add(new Content(line!, ChatMessageRole.User));
                var response = await chatSession.GenerateContentAsync(new GenerateContentRequest(messages));
                var chatResponse = response.Text();

                Console.WriteLine(chatResponse);
                messages.Add(new Content(chatResponse!, ChatMessageRole.Assistant));

                Console.WriteLine("\n\n");
            }
        }
    }
}
