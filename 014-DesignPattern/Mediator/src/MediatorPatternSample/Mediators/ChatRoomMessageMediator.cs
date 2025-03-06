using MediatorPatternSample.Colleagues;

namespace MediatorPatternSample.Mediators
{
    public class ChatRoomMessageMediator : IMessageCenterMediator
    {
        protected string AccountName { get; set; }
        protected readonly List<IChannelAccount> Accounts;


        public ChatRoomMessageMediator(string accountName)
        {
            Accounts = new List<IChannelAccount>();
            AccountName = accountName;
        }

        /// <summary>
        /// Subscribe
        /// </summary>
        /// <param name="account"></param>
        public async Task JoinAsync(IChannelAccount account)
        {
            var msg = new Message($"{account.Name} 加入聊天室.");
            await BroadcastMessageAsync(msg);

            Accounts.Add(account);

            var greatingMsg = new Message("歡迎加入");
            await SendPrivateMessageAsync(account.Name, greatingMsg); 
        }

        /// <summary>
        /// Send Message to Account
        /// </summary>
        /// <param name="accountName"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task SendPrivateMessageAsync(string accountName, Message message)
        {
            var account = Accounts.FirstOrDefault(i => i.Name == accountName);
            
            if (account != null)
            {
                var msg = new Message(message).From(AccountName).To(account.Name);
                await account.ReceiveMessageAsync(msg);
            }
        }

        /// <summary>
        /// Send Message to Accounts
        /// </summary>
        /// <param name="accountNames"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task SendMessageAsync(IEnumerable<string> accountNames, Message message)
        {
            var accounts = Accounts.Where(i => accountNames.Contains(i.Name)).ToList();

            foreach (var account in accounts)
            {
                var msg = new Message(message).From(AccountName).To(account.Name);
                await account.ReceiveMessageAsync(msg);
            }
        }

        /// <summary>
        /// Broadcast Message to All Account
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task BroadcastMessageAsync(Message message)
        {
            foreach (var account in Accounts)
            {
                var msg = new Message(message).From(AccountName).To(account.Name);
                await account.ReceiveMessageAsync(msg);
            }
        }
    }
}
