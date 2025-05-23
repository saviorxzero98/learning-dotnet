using Microsoft.Extensions.Configuration;
using OpenAI.Chat;

namespace GithubModelSample.Samples
{
    public class OpenAiCompleteChatStreamingSample : OpenAiSampleBase
    {
        public OpenAiCompleteChatStreamingSample(IConfiguration configuration) : base(configuration)
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

                var completionUpdates = chatClient.CompleteChatStreamingAsync(messages);

                var assistantMessage = string.Empty;

                await foreach (StreamingChatCompletionUpdate completionUpdate in completionUpdates)
                {
                    if (completionUpdate.ContentUpdate.Count > 0)
                    {
                        foreach (var message in completionUpdate.ContentUpdate)
                        {
                            assistantMessage += message.Text;
                            Console.Write($"{message.Text}");
                        }
                    }
                }

                messages.Add(new AssistantChatMessage(assistantMessage));

                Console.WriteLine("\n\n");
            }
        }
    }
}
