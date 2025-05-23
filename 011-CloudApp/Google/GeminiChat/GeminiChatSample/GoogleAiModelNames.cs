namespace GeminiChatSample
{
    public static class GoogleAiModelNames
    {
        /// <summary>
        /// Free: 15 RPM / 1500 RPD
        /// </summary>
        public const string GEMINI_1_5_FLASH_8B = "gemini-1.5-flash-8b";

        /// <summary>
        /// Free: 15 RPM / 1500 RPD
        /// </summary>
        public const string GEMINI_1_5_FLASH = "gemini-1.5-flash";

        /// <summary>
        /// Free: 2 RPM / 50 RPD
        /// </summary>
        public const string GEMINI_1_5_PRO = "gemini-1.5-pro";

        /// <summary>
        /// Free: 30 RPM / 1500 RPD
        /// </summary>
        public const string GEMINI_2_0_FLASH_LITE = "gemini-2.0-flash-lite";

        /// <summary>
        /// Free: 15 RPM / 1500 RPD
        /// </summary>
        public const string GEMINI_2_0_FLASH = "gemini-2.0-flash";

        /// <summary>
        /// 取得 Models
        /// </summary>
        /// <param name="modelName"></param>
        /// <returns></returns>
        public static string GetModel(string modelName)
        {
            return $"models/{modelName}";
        }
    }
}
