using System.Security.Cryptography;
using System.Text;

namespace BotGame.GuessNumberGame.Games
{
    /// <summary>
    /// 亂數產生器
    /// </summary>
    public class RandomGenerator
    {
        private const uint DefaultBytesNumber = 4;
        private const int DoublePrecision = 1000000000;

        public uint BytesNumber { get; set; }
        private RNGCryptoServiceProvider _provider;

        public RandomGenerator()
        {
            BytesNumber = DefaultBytesNumber;
            _provider = new RNGCryptoServiceProvider();
        }
        public RandomGenerator(uint bytesNumber)
        {
            BytesNumber = bytesNumber > 0 ? bytesNumber : DefaultBytesNumber;
            _provider = new RNGCryptoServiceProvider();
        }


        /// <summary>
        /// 產生一個正整數的亂數
        /// </summary>
        public int Next()
        {
            byte[] randomBytes = NextBytes();
            int value = BitConverter.ToInt32(randomBytes, 0);

            if (value < 0)
            {
                value = -value;
            }
            return value;
        }

        /// <summary>
        /// 產生一個介於 0 ~ max 的整數
        /// </summary>
        /// <param name="max"></param>
        /// <returns></returns>
        public int Next(int max)
        {
            if (max < 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            byte[] randomBytes = NextBytes();
            int value = BitConverter.ToInt32(randomBytes, 0);
            value %= max + 1;

            if (value < 0)
            {
                value = -value;
            }
            return value;
        }

        /// <summary>
        /// 產生一個介於 min ~ max 的整數
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public int Next(int min, int max)
        {
            if (max < min)
            {
                throw new ArgumentOutOfRangeException();
            }

            int value = Next(max - min) + min;
            return value;
        }

        /// <summary>
        /// 產生一個介於 0.0 ~ 1.0 的浮點數
        /// </summary>
        /// <returns></returns>
        public double NextDouble()
        {
            int randInteger = Next(0, DoublePrecision);
            double value = Convert.ToDouble(randInteger) / DoublePrecision;
            return value;
        }

        /// <summary>
        /// 產生一個介於 0 ~ max 的浮點數
        /// </summary>
        /// <param name="max"></param>
        /// <returns></returns>
        public double NextDouble(double max)
        {
            if (max < 0.0 || double.IsPositiveInfinity(max))
            {
                throw new ArgumentOutOfRangeException();
            }

            double randDouble = NextDouble();
            double value = randDouble * max;
            return value;
        }

        /// <summary>
        /// 產生一個介於 min ~ max 的浮點數
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public double NextDouble(double min, double max)
        {
            if (max < min || double.IsPositiveInfinity(max - min))
            {
                throw new ArgumentOutOfRangeException();
            }

            double randDouble = NextDouble();
            double value = min + randDouble * (max - min);
            return value;
        }

        /// <summary>
        /// 產生一組 Bytes
        /// </summary>
        /// <returns></returns>
        public byte[] NextBytes()
        {
            byte[] randomBytes = new byte[BytesNumber];
            _provider.GetBytes(randomBytes);
            return randomBytes;
        }

        /// <summary>
        /// 產生一組 Boolean
        /// </summary>
        /// <returns></returns>
        public bool NextBoolean()
        {
            int randInteger = Next(0, 1);
            return randInteger == 1;
        }

        /// <summary>
        /// 產生一組 String
        /// </summary>
        /// <param name="length"></param>
        /// <param name="charSet"></param>
        /// <returns></returns>
        public string NextString(int length = 16, string charSet = RandomCharSet.Default)
        {
            if (length < 0 || string.IsNullOrEmpty(charSet))
            {
                throw new ArgumentOutOfRangeException();
            }

            StringBuilder text = new StringBuilder();

            for (var i = 0; i < length; i++)
            {
                text.Append(charSet[Next(0, charSet.Length - 1)]);
            }

            return text.ToString();
        }

        /// <summary>
        /// 隨機取出陣列資料
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="count"></param>
        /// <param name="isDistinct"></param>
        /// <returns></returns>
        public List<T> TakeRandomItem<T>(IEnumerable<T> data, int count = 1, bool isDistinct = false)
        {
            var takeList = new List<T>();

            if (data == null)
            {
                return takeList;
            }

            var itemList = new List<T>(data);
            for (int i = 0; i < count; i++)
            {
                int maxIndex = itemList.Count - 1;
                if (maxIndex >= 0)
                {
                    int randomIndex = Next(0, maxIndex);
                    var takeItem = itemList[randomIndex];
                    takeList.Add(takeItem);

                    if (isDistinct)
                    {
                        itemList.Remove(takeItem);
                    }
                }
            }
            return takeList;
        }


        /// <summary>
        /// 亂數字元集
        /// </summary>
        public static class RandomCharSet
        {
            public const string Default = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            public const string Digit = "0123456789";
            public const string Hex = "0123456789ABCDEF";
            public const string Letter = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
        }
    }
}
