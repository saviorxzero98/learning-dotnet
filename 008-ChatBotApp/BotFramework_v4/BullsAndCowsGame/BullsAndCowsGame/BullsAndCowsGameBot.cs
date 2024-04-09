using BotGame.BullsAndCowsGame.Accessors;
using BotGame.BullsAndCowsGame.Dialogs;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Schema;

namespace BotGame.BullsAndCowsGame
{
    public class BullsAndCowsGameBot : IBot
    {
        private readonly DialogStateAccessors _dialogAccessors;
        private readonly DialogSet _dialogs;

        public BullsAndCowsGameBot(IBotDataBag botDataBag)
        {
            // 建立Bot Data Accessors
            _dialogAccessors = new DialogStateAccessors(botDataBag);

            // 建立對話流程
            _dialogs = ConfigureDialogSet(_dialogAccessors);
        }

        /// <summary>
        /// 設定對話
        /// </summary>
        /// <param name="accessors"></param>
        /// <returns></returns>
        protected DialogSet ConfigureDialogSet(DialogStateAccessors accessors)
        {
            var dialogs = new DialogSet(accessors.DialogState);

            dialogs.Add(new RootDialog(nameof(RootDialog)));

            return dialogs;
        }


        /// <summary>
        /// 訊息入口點
        /// </summary>
        /// <param name="turnContext"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task OnTurnAsync(ITurnContext turnContext, CancellationToken cancellationToken = default(CancellationToken))
        {
            switch (turnContext.Activity.Type)
            {
                case ActivityTypes.Message:
                    await HandleMessage(turnContext);
                    break;
                case ActivityTypes.ConversationUpdate:
                    await HandleConversationUpdate(turnContext);
                    break;
            }
        }

        /// <summary>
        /// 處理 Conversation Update Message
        /// </summary>
        /// <param name="turnContext"></param>
        /// <returns></returns>
        public async Task HandleConversationUpdate(ITurnContext turnContext)
        {
            if (turnContext.Activity.MembersAdded.Any(o => o.Id == turnContext.Activity.From.Id))
            {
                await turnContext.SendActivityAsync($"哈囉!");
            }
        }

        /// <summary>
        /// 處理 Message
        /// </summary>
        /// <param name="turnContext"></param>
        /// <returns></returns>
        public async Task HandleMessage(ITurnContext turnContext)
        {
            await HandleDialog(turnContext);
        }

        /// <summary>
        /// 處理 Dialog
        /// </summary>
        /// <param name="turnContext"></param>
        /// <returns></returns>
        public async Task HandleDialog(ITurnContext turnContext)
        {
            // (1) 建立DialogContext
            var dialogContext = await _dialogs.CreateContextAsync(turnContext);

            // (2) 繼續對話 (Continue Dialog)，取得 Dialog Status
            var dialogTurnResult = await dialogContext.ContinueDialogAsync();

            // (3) 檢查 Dialog Status
            if (dialogTurnResult.Status == DialogTurnStatus.Empty)
            {
                // 如果為 Empty 表示尚未開始對話，因此在這裡實作要開始的對話 (Begin Dialog or Prompt Dialog)
                await dialogContext.BeginDialogAsync(nameof(RootDialog));
            }

            // (4) 儲存 Dialog State (預設存放在 ConversationState)
            // NOTE: 如果有設定 AutoSaveBotStateMiddleware 可以不需寫這一行
            //await _dialogAccessors.ConversationData.SaveChangesAsync(turnContext);
        }
    }
}
