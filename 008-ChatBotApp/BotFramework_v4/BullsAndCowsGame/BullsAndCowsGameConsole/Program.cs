using BotGame.BullsAndCowsGame;
using BotGame.BullsAndCowsGame.Accessors;
using Microsoft.Bot.Builder;

namespace BotGame.BullsAndCowsGameConsole
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;

            // Bot 資料存放
            var storage = new MemoryStorage();

            // 建立 Bot
            var bot = new BullsAndCowsGameBot(new BotDataBag(storage));

            // 建立 Bot Console Channel Connector Client
            var chennelClient = new ConsoleChannelClient(bot, storage);

            // 開始與 Bot 對話
            await chennelClient.BeginConversationAsync(Guid.NewGuid().ToString());
        }
    }
}
