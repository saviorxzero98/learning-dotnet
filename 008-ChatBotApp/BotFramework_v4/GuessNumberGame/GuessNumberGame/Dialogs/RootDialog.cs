using Microsoft.Bot.Builder.Dialogs;

namespace BotGame.GuessNumberGame.Dialogs
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
            AddDialog(new GuessNumberGameDialog(nameof(GuessNumberGameDialog)));
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
            return await innerDc.BeginDialogAsync(nameof(GuessNumberGameDialog), new GameOption()
            {
                State = GameState.NewGame,
                Data = null
            });
        }
    }
}
