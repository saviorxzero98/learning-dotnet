using MediatorPatternSample.Colleagues;

namespace MediatorPatternSample.Mediators
{
    public interface IMessageCenterMediator
    {
        /// <summary>
        /// Subscribe
        /// </summary>
        /// <param name="account"></param>
        Task JoinAsync(IChannelAccount account);

        /// <summary>
        /// Send Message to Accounts
        /// </summary>
        /// <param name="accountNames"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        Task SendMessageAsync(IEnumerable<string> accountNames, Message message);

        /// <summary>
        /// Send Message to Account
        /// </summary>
        /// <param name="accountName"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        Task SendPrivateMessageAsync(string accountName, Message message);

        /// <summary>
        /// Broadcast Message to All Account
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        Task BroadcastMessageAsync(Message message);
    }
}
