using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;

namespace BotGame.BullsAndCowsGame.Accessors
{
    /// <summary>
    /// 對話上下文狀態存取
    /// </summary>
    public class DialogStateAccessors : BaseAccessors
    {
        public IStatePropertyAccessor<DialogState> DialogState { get; set; }

        public DialogStateAccessors(IBotDataBag botDataBag) : base(botDataBag)
        {
            DialogState = botDataBag.ConversationData.CreateProperty<DialogState>(nameof(DialogState));
        }
    }
}
