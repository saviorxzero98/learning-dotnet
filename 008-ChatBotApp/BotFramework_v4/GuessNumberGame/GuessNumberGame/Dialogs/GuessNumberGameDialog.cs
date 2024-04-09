using BotGame.GuessNumberGame.Games;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;

namespace BotGame.GuessNumberGame.Dialogs
{
    public class GuessNumberGameDialog : BaseGameDialog
    {
        public GuessNumberGameDialog(string dialogId) : base(dialogId)
        {

        }

        /// <summary>
        /// 對話 ID
        /// </summary>
        protected override string DialogId => nameof(GuessNumberGameDialog);

        /// <summary>
        /// 遊戲初始化
        /// </summary>
        /// <param name="dc"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public override async Task<GameResult> OnGameInitializeAsync(DialogContext dc, GameOption options)
        {
            ITurnContext turnContext = dc.Context;

            await turnContext.SendActivityAsync("開始猜數字");

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
                var game = GuessNumberGameRunner.NewGame(0, 100);

                await stepContext.Context.SendActivityAsync("請輸入 0 ~ 100 數字");

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
                var game = (GuessNumberGameRunner)options.Data;

                if (int.TryParse(text, out int number))
                {
                    var result = game.Guess(number);

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
                }
                else
                {
                    await stepContext.Context.SendActivityAsync($"Error");
                }

                return new GameResult()
                {
                    NextState = GameState.ResumeGame,
                    Data = options.Data
                };
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
