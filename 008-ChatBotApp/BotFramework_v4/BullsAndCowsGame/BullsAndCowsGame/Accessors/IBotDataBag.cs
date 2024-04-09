using Microsoft.Bot.Builder;

namespace BotGame.BullsAndCowsGame.Accessors
{
    public interface IBotDataBag
    {
        /// <summary>
        /// 使用者資料
        /// </summary>
        UserState UserData { get; }

        /// <summary>
        /// 聊天室對話資料
        /// </summary>
        ConversationState ConversationData { get; }

        /// <summary>
        /// 聊天室使用者對話資料
        /// </summary>
        PrivateConversationState PrivateConversationData { get; }

        BotStateSet BotStateSet { get; }
    }
}
