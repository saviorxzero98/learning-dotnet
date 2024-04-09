using BotGame.GuessNumberGame.Accessors;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Integration.AspNet.Core;

namespace BotGame.GuessNumberGameWeb.Bots
{
    public class MiddlewareAndErrorHandler : BotFrameworkHttpAdapter
    {
        private readonly IBotDataBag _botDataBag;
        private readonly IConfiguration _configuration;
        private readonly ILoggerFactory _loggerFactory;
        private readonly ILogger<BotFrameworkHttpAdapter> _logger;

        public MiddlewareAndErrorHandler(IBotDataBag botDataBag,
                                         IConfiguration configuration,
                                         ILoggerFactory loggerFactory,
                                         ILogger<BotFrameworkHttpAdapter> logger) : base(configuration, logger)
        {
            _botDataBag = botDataBag;
            _configuration = configuration;
            _loggerFactory = loggerFactory;
            _logger = logger;

            // 設定 Middleware
            UseMiddlewares();

            // 設定 Bot 發生錯誤的處理
            OnTurnError = OnTurnErrorAsync;
        }

        /// <summary>
        /// 設定 Middleware
        /// </summary>
        protected void UseMiddlewares()
        {
            // 加入 AutoSaveBotStateMiddleware (自動儲存 Bot State)
            Use(new AutoSaveStateMiddleware(_botDataBag.BotStateSet));
        }

        /// <summary>
        /// 設定 Bot 發生錯誤的處理
        /// </summary>
        /// <param name="turnContext"></param>
        /// <param name="exception"></param>
        /// <returns></returns>
        protected async Task OnTurnErrorAsync(ITurnContext turnContext, Exception exception)
        {
            _logger.LogError(exception.Message, exception);

            // 發送錯誤訊息
            await turnContext.SendActivityAsync("好像發生錯誤了");
        }
    }
}
