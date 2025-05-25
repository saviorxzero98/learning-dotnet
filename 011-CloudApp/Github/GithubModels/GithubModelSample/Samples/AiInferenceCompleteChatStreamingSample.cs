using Azure.AI.Inference;
using Microsoft.Extensions.Configuration;

namespace GithubModelSample.Samples
{
    public class AiInferenceCompleteChatStreamingSample : AiInferenceSampleBase
    {
        public AiInferenceCompleteChatStreamingSample(IConfiguration configuration) : base(configuration)
        {
        }

        protected override async Task HandleAsync(ChatCompletionsClient chatClient)
        {
            var messages = new List<ChatRequestMessage>()
            {
                new ChatRequestSystemMessage("請以繁體中文回答問題")
            };

            while (true)
            {
                Console.WriteLine("請輸入您的問題：");
                var line = Console.ReadLine();

                if (line == "exit")
                {
                    break;
                }

                messages.Add(new ChatRequestUserMessage(line));

                var options = new ChatCompletionsOptions(messages)
                {
                    Model = GetModelName()
                };

                StreamingResponse<StreamingChatCompletionsUpdate> completionUpdates = await chatClient.CompleteStreamingAsync(options);

                var assistantMessage = string.Empty;

                await foreach (StreamingChatCompletionsUpdate message in completionUpdates)
                {
                    if (!string.IsNullOrEmpty(message.ContentUpdate))
                    {
                        assistantMessage += message.ContentUpdate;
                        Console.Write($"{message.ContentUpdate}");
                    }
                }

                messages.Add(new ChatRequestAssistantMessage(assistantMessage));

                Console.WriteLine("\n\n");
            }
        }
    }
}
