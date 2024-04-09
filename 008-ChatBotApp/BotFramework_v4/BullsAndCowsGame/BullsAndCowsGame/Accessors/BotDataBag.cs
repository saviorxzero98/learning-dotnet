using Microsoft.Bot.Builder;

namespace BotGame.BullsAndCowsGame.Accessors
{
    public class BotDataBag : IBotDataBag
    {
        public UserState UserData { get; }
        public ConversationState ConversationData { get; }
        public PrivateConversationState PrivateConversationData { get; }
        public BotStateSet BotStateSet { get => new BotStateSet(UserData, ConversationData, PrivateConversationData); }

        public BotDataBag(IStorage storage)
        {
            UserData = new UserState(storage);
            ConversationData = new ConversationState(storage);
            PrivateConversationData = new PrivateConversationState(storage);
        }
    }
}
