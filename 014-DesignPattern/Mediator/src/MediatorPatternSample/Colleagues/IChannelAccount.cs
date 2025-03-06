namespace MediatorPatternSample.Colleagues
{
    public interface IChannelAccount
    {
        /// <summary>
        /// Account Name
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Channel Type
        /// </summary>
        string ChannelType { get; }

        /// <summary>
        /// Receive Message
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        Task ReceiveMessageAsync(Message message);
    }
}
