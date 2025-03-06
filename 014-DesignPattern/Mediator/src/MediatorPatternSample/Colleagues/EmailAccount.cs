namespace MediatorPatternSample.Colleagues
{
    public class EmailAccount : IChannelAccount
    {
        public const string ChannelName = "email";

        public string Name { get; protected set; }

        public string ChannelType => ChannelName;


        public EmailAccount(string name)
        {
            Name = name;
        }

        /// <summary>
        /// Receive Message
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public Task ReceiveMessageAsync(Message message)
        {
            Console.WriteLine($"----------------------------------------");
            Console.WriteLine($"Channel: {ChannelType}");
            Console.WriteLine($"----------------------------------------");
            Console.WriteLine($"From: {message.FromName}");
            Console.WriteLine($"To: {message.RecipientName}");
            Console.WriteLine($"Subject: {message.Subject}");
            Console.WriteLine($"----------------------------------------\n");

            return Task.CompletedTask;
        }
    }
}
