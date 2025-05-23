using Azure.AI.Inference;
using Microsoft.Extensions.Configuration;

namespace GithubModelSample.Samples
{
    public class AiInferenceCompleteChatSample : AiInferenceSampleBase
    {
        public AiInferenceCompleteChatSample(IConfiguration configuration) : base(configuration)
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
                var response = await chatClient.CompleteAsync(options);
                var chatResponse = response.Value.Content;

                Console.WriteLine(chatResponse);
                messages.Add(new ChatRequestAssistantMessage(chatResponse));

                Console.WriteLine("\n\n");
            }
        }
    }
}
