using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;

namespace BotGame.GuessNumberGameConsole
{
    /// <summary>
    /// Chat Bot Console Adapter
    /// </summary>
    public class ConsoleAdapter : BotAdapter
    {
        public ConsoleAdapter() : base()
        {
        }

        /// <summary>
        /// Use Middleware
        /// </summary>
        /// <param name="middleware"></param>
        /// <returns></returns>
        public new ConsoleAdapter Use(IMiddleware middleware)
        {
            base.Use(middleware);
            return this;
        }

        /// <summary>
        /// Process Activity
        /// </summary>
        /// <param name="activity"></param>
        /// <param name="callback"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task ProcessActivityAsync(Activity activity, BotCallbackHandler callback, CancellationToken cancellationToken = default)
        {
            using (var context = new TurnContext(this, activity))
            {
                await RunPipelineAsync(context, callback, default(CancellationToken)).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// 發送訊息
        /// </summary>
        /// <param name="context"></param>
        /// <param name="activities"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public override async Task<ResourceResponse[]> SendActivitiesAsync(ITurnContext context, Activity[] activities, CancellationToken cancellationToken)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (activities == null)
            {
                throw new ArgumentNullException(nameof(activities));
            }

            if (activities.Length == 0)
            {
                throw new ArgumentException("Expecting one or more activities, but the array was empty.", nameof(activities));
            }

            var responses = new ResourceResponse[activities.Length];

            for (var index = 0; index < activities.Length; index++)
            {
                var activity = activities[index];

                switch (activity.Type)
                {
                    case ActivityTypes.Message:
                        IMessageActivity message = activity.AsMessageActivity();
                        if (!string.IsNullOrEmpty(message.Text))
                        {
                            Println(message.Text, ConsoleColor.Yellow);
                        }
                        break;

                    case ActivityTypesEx.Delay:
                        int delayMs = (int)((Activity)activity).Value;
                        await Task.Delay(delayMs).ConfigureAwait(false);
                        break;
                }

                responses[index] = new ResourceResponse(activity.Id);
            }

            return responses;
        }

        /// <summary>
        /// 更新訊息
        /// </summary>
        /// <param name="turnContext"></param>
        /// <param name="activity"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public override Task<ResourceResponse> UpdateActivityAsync(ITurnContext turnContext, Activity activity, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 刪除訊息
        /// </summary>
        /// <param name="turnContext"></param>
        /// <param name="reference"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public override Task DeleteActivityAsync(ITurnContext turnContext, ConversationReference reference, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }


        #region Private

        /// <summary>
        /// 顯示文字
        /// </summary>
        /// <param name="text"></param>
        protected void Println(string text)
        {
            Console.WriteLine(text);
        }

        /// <summary>
        /// 顯示文字
        /// </summary>
        /// <param name="text"></param>
        /// <param name="textColor"></param>
        protected void Println(string text, ConsoleColor textColor)
        {
            ConsoleColor orgtextColor = Console.ForegroundColor;

            Console.ForegroundColor = textColor;
            Console.WriteLine(text);

            Console.ForegroundColor = orgtextColor;
        }

        #endregion
    }
}
