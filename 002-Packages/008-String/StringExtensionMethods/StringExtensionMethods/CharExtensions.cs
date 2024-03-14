namespace StringExtensionMethods
{
    public static class CharExtensions
    {
        /// <summary>
        /// 字元轉成大寫
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static char ToUpper(this char value)
        {
            return char.ToUpper(value);
        }

        /// <summary>
        /// 字元轉成小寫
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static char ToLower(this char value)
        {
            return char.ToLower(value);
        }

        /// <summary>
        /// 字元轉成全形
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static char ToFullWidth(this char value)
        {
            if (value == 32)
            {
                return (char)12288;
            }

            if (value < 127)
            {
                return (char)(value + 65248);
            }

            return value;
        }

        /// <summary>
        /// 字元轉成半形
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static char ToHalfWidth(this char value)
        {
            if (value == 12288)
            {
                return (char)32;
            }

            if (value > 65280 && value < 65375)
            {
                return (char)(value - 65248);
            }
            return value;
        }
    }
}
