using BotGame.GuessNumberGame.Accessors;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;

namespace BotGame.GuessNumberGameConsole
{
    /// <summary>
    /// Console Channel Connection Client
    /// </summary>
    public class ConsoleChannelClient
    {
        private const string DialogStateKey = "DialogState";

        public readonly ConsoleAdapter _botAdapter;
        protected BotCallbackHandler _callback;
        protected IBotDataBag _botDataBag;

        public IBot Bot { get; protected set; }


        public ConsoleChannelClient(IBot bot)
        {
            Bot = bot;
            _botAdapter = new ConsoleAdapter();
            UseStorage(new MemoryStorage());
            _callback = async (turnContext, cancellationToken) => await Bot.OnTurnAsync(turnContext);
        }
        public ConsoleChannelClient(IBot bot, IStorage storage)
        {
            Bot = bot;
            _botAdapter = new ConsoleAdapter();
            UseStorage(storage);
            _callback = async (turnContext, cancellationToken) => await Bot.OnTurnAsync(turnContext);
        }


        #region Builder

        /// <summary>
        /// 設定 Storage
        /// </summary>
        /// <param name="storage"></param>
        /// <returns></returns>
        public ConsoleChannelClient UseStorage(IStorage storage)
        {
            if (storage != null)
            {
                _botDataBag = new BotDataBag(storage);
                _botAdapter.Use(new AutoSaveStateMiddleware(_botDataBag.BotStateSet));
            }
            return this;
        }

        public ConsoleChannelClient SetBot(IBot bot)
        {
            Bot = bot;
            return this;
        }

        public ConsoleChannelClient AddMiddleware(IMiddleware middleware)
        {
            if (middleware != null)
            {
                _botAdapter.Use(middleware);
            }
            return this;
        }

        #endregion

        /// <summary>
        /// 開始對話
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task BeginConversationAsync(string userId)
        {
            ConversationReference cr = new ConversationReference()
            {
                ActivityId = Guid.NewGuid().ToString(),
                ChannelId = "concole",
                Conversation = new ConversationAccount(id: Guid.NewGuid().ToString()),
                Bot = new ChannelAccount()
                {
                    Id = "bot",
                    Name = "Bot"
                },
                User = new ChannelAccount()
                {
                    Id = userId,
                    Name = "User"
                }
            };

            await BeginConversationAsync(cr);
        }

        /// <summary>
        /// 開始對話
        /// </summary>
        /// <param name="conversation"></param>
        /// <returns></returns>
        public async Task BeginConversationAsync(ConversationReference conversation)
        {
            Activity conversationUpdate = CreateConversationUpdate(conversation);
            await SendMessageAsync(conversationUpdate);

            while (true)
            {
                var text = Console.ReadLine();
                if (text == null)
                {
                    break;
                }

                Activity message = CreateMessage(conversation, text);

                await SendMessageAsync(message);
            }
        }

        /// <summary>
        /// 發送訊息
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        protected async Task SendMessageAsync(Activity message)
        {
            await _botAdapter.ProcessActivityAsync(message, _callback);
        }

        /// <summary>
        /// 建立訊息
        /// </summary>
        /// <param name="cr"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        private Activity CreateMessage(ConversationReference cr, string message)
        {
            var activity = new Activity()
            {
                Text = message,

                ChannelId = cr.ChannelId,
                From = cr.User,
                Recipient = cr.Bot,
                Conversation = cr.Conversation,
                Timestamp = DateTime.UtcNow,
                Id = Guid.NewGuid().ToString(),
                Type = ActivityTypes.Message,
            };

            return activity;
        }

        /// <summary>
        /// 建立對話開始訊息
        /// </summary>
        /// <param name="cr"></param>
        /// <returns></returns>
        private Activity CreateConversationUpdate(ConversationReference cr)
        {
            var activity = new Activity()
            {
                ChannelId = cr.ChannelId,
                From = cr.User,
                Recipient = cr.Bot,
                Conversation = cr.Conversation,
                Timestamp = DateTime.UtcNow,
                Id = Guid.NewGuid().ToString(),
                Type = ActivityTypes.ConversationUpdate,

                MembersAdded = new List<ChannelAccount>()
                {
                    cr.User,
                    cr.Bot
                }
            };

            return activity;
        }
    }
}
