using Microsoft.Bot.Builder.Dialogs;

namespace BotGame.GuessNumberGame.Dialogs
{
    /// <summary>
    /// 遊戲子對話基底
    /// </summary>
    public abstract class BaseGameDialog : ComponentDialog
    {
        protected const string GameFlow = "GameFlow";

        protected abstract string DialogId { get; }

        public BaseGameDialog(string dialogId) : base(dialogId)
        {
            ConfigureDialogSet();
        }

        /// <summary>
        /// 設定對話
        /// </summary>
        protected void ConfigureDialogSet()
        {
            // 加入問答的處理對話的步驟
            var steps = new List<WaterfallStep>()
            {
                BeginGameAsync,
                ResumeGameAsync
            };

            AddDialog(new WaterfallDialog($"{DialogId}-{GameFlow}", steps));
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
            GameOption gameOptions;
            GameResult gameResults;
            if (options is GameOption)
            {
                gameOptions = (GameOption)options;
                gameResults = await OnGameInitializeAsync(innerDc, gameOptions);
            }
            else
            {
                gameOptions = new GameOption()
                {
                    State = GameState.NewGame,
                    Data = options
                };
                gameResults = await OnGameInitializeAsync(innerDc, gameOptions);
            }

            if (gameResults == null || gameResults.NextState == GameState.EndGame)
            {
                return await innerDc.EndDialogAsync();
            }
            else
            {
                return await innerDc.BeginDialogAsync($"{DialogId}-{GameFlow}", gameOptions);
            }
        }


        #region Abstruct Method

        /// <summary>
        /// 遊戲初始化
        /// </summary>
        /// <param name="dc"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public abstract Task<GameResult> OnGameInitializeAsync(DialogContext dc, GameOption options);

        /// <summary>
        /// 遊戲開始時
        /// </summary>
        /// <param name="stepContext"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public abstract Task<GameResult> OnGameBeginAsync(WaterfallStepContext stepContext, GameOption options);

        /// <summary>
        /// 遊戲重啟時
        /// </summary>
        /// <param name="stepContext"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public abstract Task<GameResult> OnGameResumeAsync(WaterfallStepContext stepContext, GameOption options);

        #endregion


        #region Waterfall Step

        /// <summary>
        /// 遊戲開始
        /// </summary>
        /// <param name="stepContext"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        private async Task<DialogTurnResult> BeginGameAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            var gameOptions = (GameOption)stepContext.Options;

            GameResult gameResults = await OnGameBeginAsync(stepContext, gameOptions);

            if (gameResults == null || gameResults.NextState == GameState.EndGame)
            {
                return await stepContext.EndDialogAsync();
            }
            else
            {
                stepContext.Values.Add($"{nameof(GameResult)}{nameof(GameResult.Data)}", gameResults.Data);
                return EndOfTurn;
            }
        }

        /// <summary>
        /// 遊戲重啟
        /// </summary>
        /// <param name="stepContext"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        private async Task<DialogTurnResult> ResumeGameAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            if (stepContext.Values.TryGetValue($"{nameof(GameResult)}{nameof(GameResult.Data)}", out object gameResult))
            {
                GameResult gameResults = await OnGameResumeAsync(stepContext, new GameOption()
                {
                    State = GameState.ContinueGame,
                    Data = gameResult
                });

                if (gameResults != null && gameResults.NextState == GameState.ResumeGame)
                {
                    var resumeGameOptions = new GameOption()
                    {
                        State = GameState.ResumeGame,
                        Data = gameResults.Data
                    };

                    return await stepContext.ReplaceDialogAsync($"{DialogId}-{GameFlow}", resumeGameOptions, cancellationToken);
                }
                else if (gameResults != null && gameResults.NextState == GameState.NewGame)
                {
                    var resumeGameOptions = new GameOption()
                    {
                        State = GameState.NewGame,
                        Data = gameResults.Data
                    };

                    return await stepContext.BeginDialogAsync($"{DialogId}-{GameFlow}", resumeGameOptions, cancellationToken);
                }
            }

            return await stepContext.EndDialogAsync();
        }

        #endregion
    }

    /// <summary>
    /// 遊戲狀態
    /// </summary>
    public enum GameState
    {
        NewGame,
        ContinueGame,
        ResumeGame,
        EndGame
    }

    /// <summary>
    /// 遊戲對話狀態和資料
    /// </summary>
    public class GameOption
    {
        public GameState State { get; set; }

        public object Data { get; set; }
    }

    /// <summary>
    /// 遊戲狀態轉換
    /// </summary>
    public class GameResult
    {
        public GameState NextState { get; set; }

        public object Data { get; set; }
    }
}
