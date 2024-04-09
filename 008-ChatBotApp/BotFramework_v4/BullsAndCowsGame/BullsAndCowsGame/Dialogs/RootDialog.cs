using Microsoft.Bot.Builder.Dialogs;

namespace BotGame.BullsAndCowsGame.Dialogs
{
    /// <summary>
    /// 根對話
    /// </summary>
    public class RootDialog : ComponentDialog
    {
        public RootDialog(string dialogId) : base(dialogId)
        {
            ConfigureDialogSet();
        }

        /// <summary>
        /// 設定子對話
        /// </summary>
        protected void ConfigureDialogSet()
        {
            AddDialog(new BullsAndCowsGameDialog(nameof(BullsAndCowsGameDialog)));
        }

        /// <summary>
        /// 對話開始時
        /// </summary>
        /// <param name="innerDc"></param>
        /// <param name="options"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        protected override async Task<DialogTurnResult> OnBeginDialogAsync(DialogContext innerDc, object options, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await innerDc.BeginDialogAsync(nameof(BullsAndCowsGameDialog), new GameOption()
            {
                State = GameState.NewGame,
                Data = null
            });
        }
    }
}
