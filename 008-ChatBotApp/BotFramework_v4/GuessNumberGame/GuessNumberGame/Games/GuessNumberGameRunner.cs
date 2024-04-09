namespace BotGame.GuessNumberGame.Games
{
    public class GuessNumberGameRunner
    {
        public const int DefaultMax = 999;
        public const int DefaultMin = 0;

        public int Max { get; set; }
        public int Min { get; set; }
        public int Answer { get; set; }
        public int GuestCount { get; set; }

        public GuessNumberGameRunner()
        {
        }

        /// <summary>
        /// 新遊戲
        /// </summary>
        /// <returns></returns>
        public static GuessNumberGameRunner NewGame()
        {
            var random = new RandomGenerator();
            var answer = random.Next(DefaultMin, DefaultMax);

            return new GuessNumberGameRunner()
            {
                Max = DefaultMax,
                Min = DefaultMin,
                Answer = answer,
                GuestCount = 0
            };
        }

        /// <summary>
        /// 新遊戲
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static GuessNumberGameRunner NewGame(int min, int max)
        {
            var random = new RandomGenerator();
            var answer = random.Next(min, max);

            return new GuessNumberGameRunner()
            {
                Max = max,
                Min = min,
                Answer = answer,
                GuestCount = 0
            };
        }

        /// <summary>
        /// 猜數字
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public GuessNumberGameResult Guess(int number)
        {
            GuestCount++;

            if (number > Answer)
            {
                Max = Math.Min(number, Max);
            }
            else if (number < Answer)
            {
                Min = Math.Max(number, Min);
            }

            bool isWin = (number == Answer);

            return new GuessNumberGameResult(Min, Max, isWin, GuestCount);
        }

        /// <summary>
        /// 取得數字答案
        /// </summary>
        /// <returns></returns>
        public int GetAnswer()
        {
            return Answer;
        }
    }

    /// <summary>
    /// 遊戲狀態和結果
    /// </summary>
    public class GuessNumberGameResult
    {
        public int Max { get; set; }

        public int Min { get; set; }

        public int GuestCount { get; set; }

        public bool IsWin { get; set; }

        public string Prompt { get; set; }

        public GuessNumberGameResult(int min, int max, bool isWin, int guestCount)
        {
            Max = max;
            Min = min;
            Prompt = GetString();
            GuestCount = guestCount;
            IsWin = isWin;
        }

        public string GetString()
        {
            return $"{Min} ~ {Max}";
        }
    }
}
