using Microsoft.AspNetCore.Mvc;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Integration.AspNet.Core;

namespace BotGame.GuessNumberGameWeb.Controllers
{
    [Route("api")]
    [ApiController]
    public class BotMessagesController : ControllerBase
    {
        public const string ControllerName = "Messages";

        private readonly IBotFrameworkHttpAdapter Adapter;
        private readonly IBot Bot;

        public BotMessagesController(IBotFrameworkHttpAdapter adapter, IBot bot)
        {
            Adapter = adapter;
            Bot = bot;
        }

        /// <summary>
        /// /api/messages
        /// </summary>
        /// <returns></returns>
        [Route("messages")]
        [HttpPost]
        public async Task PostMessagesAsync()
        {
            await Adapter.ProcessAsync(Request, Response, Bot);
        }
    }
}
