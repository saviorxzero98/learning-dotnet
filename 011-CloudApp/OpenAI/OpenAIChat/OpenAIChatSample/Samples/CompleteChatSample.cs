using Microsoft.Extensions.Configuration;
using OpenAI.Chat;

namespace OpenAIChatSample.Samples
{
    public class CompleteChatSample : DemoSampleBase
    {
        public CompleteChatSample(IConfiguration configuration) : base(configuration)
        {
        }

        protected override async Task HandleAsync(ChatClient chatClient)
        {
            var messages = new List<ChatMessage>()
            {
                new SystemChatMessage("請以繁體中文回答問題")
            };

            while (true)
            {
                Console.WriteLine("請輸入您的問題：");
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
    }
}
