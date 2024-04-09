namespace BotGame.BullsAndCowsGame.Games
{
    public class BullsAndCowsGameRunner
    {
        public List<double> NumberChars { get; set; } = new List<double>();
        public int GuessCount { get; set; }

        /// <summary>
        /// 開始新遊戲
        /// </summary>
        /// <returns></returns>
        public static BullsAndCowsGameRunner NewGame()
        {
            var random = new RandomGenerator();

            string numberCharSet = "0123456789";
            List<double> numbers = numberCharSet.ToCharArray()
                                                .Select(c => Char.GetNumericValue(c))
                                                .ToList();

            var numberChars = random.TakeRandomItem(numbers, 4, true);

            return new BullsAndCowsGameRunner()
            {
                NumberChars = numberChars,
                GuessCount = 0
            };
        }

        /// <summary>
        /// 猜數字
        /// </summary>
        /// <param name="guessNumberText"></param>
        /// <returns></returns>
        public BullsAndCowsResult Guess(string guessNumberText)
        {
            int countA = 0;
            int countB = 0;

            GuessCount++;

            if (guessNumberText.Length != 4 && !int.TryParse(guessNumberText, out int guessNumber))
            {
                return new BullsAndCowsResult(countA, countB, GuessCount);
            }

            List<double> guessNumberChars = guessNumberText.ToCharArray()
                                                           .Select(c => Char.GetNumericValue(c))
                                                           .ToList();

            for (int i = 0; i < guessNumberChars.Count; i++)
            {
                for (int j = 0; j < NumberChars.Count; j++)
                {
                    if (guessNumberChars[i] == NumberChars[j])
                    {
                        if (i == j)
                        {
                            countA++;
                        }
                        else
                        {
                            countB++;
                        }
                        break;
                    }
                }
            }

            return new BullsAndCowsResult(countA, countB, GuessCount);
        }

        public string GetAnswer()
        {
            return string.Join("", NumberChars);
        }
    }

    /// <summary>
    /// 提示遊戲結果 (幾A幾B)
    /// </summary>
    public class BullsAndCowsResult
    {
        public int CountA { get; set; }
        public int CountB { get; set; }
        public bool IsWin { get; set; }
        public string Prompt { get; set; }
        public int GuestCount { get; set; }


        public BullsAndCowsResult(int countA, int countB, int guestCount)
        {
            CountA = countA;
            CountB = countB;
            Prompt = GetString();
            IsWin = (countA == 4);
            GuestCount = guestCount;
        }

        public string GetString()
        {
            return $"{CountA}A{CountB}B";
        }
    }
}
