using Microsoft.Bot.Builder;

namespace BotGame.GuessNumberGame.Accessors
{
    /// <summary>
    /// Bot 對話狀態資料存取
    /// </summary>
    public abstract class BaseAccessors
    {
        protected IBotDataBag BotDataBag { get; }
        public UserState UserData { get { return BotDataBag.UserData; } }
        public ConversationState ConversationData { get { return BotDataBag.ConversationData; } }
        public PrivateConversationState PrivateConversationData { get { return BotDataBag.PrivateConversationData; } }

        protected BaseAccessors(IBotDataBag botDataBag)
        {
            BotDataBag = botDataBag;
        }

        /// <summary>
        /// 儲存 Bot 所有對話狀態
        /// </summary>
        /// <param name="turnContext"></param>
        /// <param name="force"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task SaveAllChangesAsync(ITurnContext turnContext, bool force = false, CancellationToken cancellationToken = default(CancellationToken))
        {
            await BotDataBag.BotStateSet.SaveAllChangesAsync(turnContext, force, cancellationToken);
        }
    }
}
