using BotGame.BullsAndCowsGame.Games;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;

namespace BotGame.BullsAndCowsGame.Dialogs
{
    public class BullsAndCowsGameDialog : BaseGameDialog
    {
        public BullsAndCowsGameDialog(string dialogId) : base(dialogId)
        {

        }

        /// <summary>
        /// 對話 ID
        /// </summary>
        protected override string DialogId => nameof(BullsAndCowsGameDialog);

        /// <summary>
        /// 遊戲初始化
        /// </summary>
        /// <param name="dc"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public override async Task<GameResult> OnGameInitializeAsync(DialogContext dc, GameOption options)
        {
            ITurnContext turnContext = dc.Context;

            await turnContext.SendActivityAsync("開始1A2B猜數字遊戲");

            return new GameResult()
            {
                NextState = GameState.NewGame,
                Data = options.Data
            };
        }

        /// <summary>
        /// 遊戲開始時
        /// </summary>
        /// <param name="stepContext"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public override async Task<GameResult> OnGameBeginAsync(WaterfallStepContext stepContext, GameOption options)
        {
            if (options.State == GameState.NewGame)
            {
                var game = BullsAndCowsGameRunner.NewGame();

                await stepContext.Context.SendActivityAsync("請輸入四位數字");

                return new GameResult()
                {
                    NextState = GameState.ContinueGame,
                    Data = game
                };
            }

            return new GameResult()
            {
                NextState = GameState.ContinueGame,
                Data = options.Data
            };
        }

        /// <summary>
        /// 遊戲重啟時
        /// </summary>
        /// <param name="stepContext"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public override async Task<GameResult> OnGameResumeAsync(WaterfallStepContext stepContext, GameOption options)
        {
            var message = stepContext.Context.Activity;
            var text = message.Text;

            if (!string.IsNullOrEmpty(text))
            {
                var game = (BullsAndCowsGameRunner)options.Data;

                var result = game.Guess(text);

                await stepContext.Context.SendActivityAsync($"{result.Prompt} ({result.GuestCount} 次)");

                if (result.IsWin)
                {
                    await stepContext.Context.SendActivityAsync("You win.\n");
                    return new GameResult()
                    {
                        NextState = GameState.NewGame,
                        Data = options.Data
                    };
                }
                else
                {
                    return new GameResult()
                    {
                        NextState = GameState.ResumeGame,
                        Data = options.Data
                    };
                }
            }
            else
            {
                return new GameResult()
                {
                    NextState = GameState.EndGame,
                    Data = options.Data
                };
            }
        }
    }
}
