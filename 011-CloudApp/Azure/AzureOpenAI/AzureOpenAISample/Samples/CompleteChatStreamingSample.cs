using Microsoft.Extensions.Configuration;
using OpenAI.Chat;

namespace AzureOpenAISample.Samples
{
    public class CompleteChatStreamingSample : DemoSampleBase
    {
        public CompleteChatStreamingSample(IConfiguration configuration) : base(configuration)
        {
        }

        protected override async Task HandleAsync(ChatClient chatClient)
        {
            var messages = new List<ChatMessage>();

            while (true)
            {
                Console.WriteLine("請輸入您的問題：");
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
    }
}
