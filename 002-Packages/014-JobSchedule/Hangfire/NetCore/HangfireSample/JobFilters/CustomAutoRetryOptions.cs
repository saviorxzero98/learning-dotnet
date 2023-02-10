using System;

namespace HangfireSample.JobFilters
{
    public class CustomAutoRetryOptions
    {
        public const int DefaultErrorRetryAttempts = 3;
        public const int DefaultIdleCheckRetryAttempts = 5;
        public const int DefaultIdleCheckDelayInSeconds = 30;

        private static readonly Func<long, int> DefaultErrorDelayInSecondsFunc = (errorAttempts) =>
        {
            var random = new Random();
            return (int)Math.Round(Math.Pow(errorAttempts - 1, 4) + 15 + random.Next(30) * errorAttempts);
        };

        public int ErrorAttempts { get; set; }
        public int ErrorDelayInSeconds
        {
            get
            {
                if (IsCustomErrorDelayInSeconds)
                {
                    return CustomErrorDelayInSeconds;
                }
                return DefaultErrorDelayInSecondsFunc.Invoke(ErrorAttempts);
            }
        }
        public bool IsCustomErrorDelayInSeconds { get => CustomErrorDelayInSeconds > 0; }
        public int CustomErrorDelayInSeconds { get; set; }

        public int IdleCheckAttempts { get; set; }
        public int IdleCheckDelayInSeconds { get; set; }

        public CustomAutoRetryOptions()
        {
            ErrorAttempts = DefaultErrorRetryAttempts;
            IdleCheckAttempts = DefaultIdleCheckRetryAttempts;
            IdleCheckDelayInSeconds = DefaultIdleCheckDelayInSeconds;
            CustomErrorDelayInSeconds = 0;
        }
        public CustomAutoRetryOptions(CustomAutoRetryOptions options)
        {
            ErrorAttempts = options.ErrorAttempts;
            IdleCheckAttempts = options.IdleCheckAttempts;
            IdleCheckDelayInSeconds = options.IdleCheckDelayInSeconds;
            CustomErrorDelayInSeconds = options.CustomErrorDelayInSeconds;
        }

        /// <summary>
        /// 設定錯誤重試
        /// </summary>
        /// <param name="attempts"></param>
        /// <returns></returns>
        public CustomAutoRetryOptions SetErrorRetry(int attempts)
        {
            ErrorAttempts = attempts;
            return this;
        }
        /// <summary>
        /// 設定錯誤重試
        /// </summary>
        /// <param name="attempts"></param>
        /// <returns></returns>
        public CustomAutoRetryOptions SetErrorRetry(int attempts, int delayInSecond)
        {
            ErrorAttempts = attempts;
            CustomErrorDelayInSeconds = delayInSecond;
            return this;
        }


        /// <summary>
        /// 設定閒置檢查重試
        /// </summary>
        /// <param name="attempts"></param>
        /// <param name="delayInSeconds"></param>
        /// <returns></returns>
        public CustomAutoRetryOptions SetIdleCheckRetry(int attempts, int delayInSeconds)
        {
            IdleCheckAttempts = attempts;
            IdleCheckDelayInSeconds = delayInSeconds;
            return this;
        }
    }
}
