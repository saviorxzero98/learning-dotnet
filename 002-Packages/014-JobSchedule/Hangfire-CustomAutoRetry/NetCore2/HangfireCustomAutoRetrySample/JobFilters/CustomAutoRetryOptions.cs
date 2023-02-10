using HangfireCustomAutoRetrySample.Toolkits;
using System;

namespace HangfireCustomAutoRetrySample.JobFilters
{
    public class CustomAutoRetryOptions
    {
        public const int DefaultErrorRetryAttempts = 3;
        public const int DefaultBusyRetryAttempts = 5;
        public const int DefaultBusyRetryDelayInSeconds = 30;

        private static readonly Func<long, int> DefaultErrorDelayInSecondsFunc = (errorAttempts) =>
        {
            var random = new RandomGenerator();
            return (int)Math.Round(Math.Pow(errorAttempts - 1, 4) + 15 + random.Next(30) * errorAttempts);
        };

        public int ErrorRetryAttempts { get; set; }
        public int ErrorRetryDelayInSeconds
        {
            get
            {
                if (IsCustomErrorDelayInSeconds)
                {
                    return CustomErrorDelayInSeconds;
                }
                return DefaultErrorDelayInSecondsFunc.Invoke(ErrorRetryAttempts);
            }
            set
            {
                CustomErrorDelayInSeconds = value;
            }
        }
        public bool IsCustomErrorDelayInSeconds { get => CustomErrorDelayInSeconds > 0; }
        public int CustomErrorDelayInSeconds { get; set; }

        public int BusyRetryAttempts { get; set; }
        public int BusyRetryDelayInSeconds { get; set; }

        public CustomAutoRetryOptions()
        {
            ErrorRetryAttempts = DefaultErrorRetryAttempts;
            BusyRetryAttempts = DefaultBusyRetryAttempts;
            BusyRetryDelayInSeconds = DefaultBusyRetryDelayInSeconds;
            CustomErrorDelayInSeconds = 0;
        }
        public CustomAutoRetryOptions(CustomAutoRetryOptions options)
        {
            ErrorRetryAttempts = options.ErrorRetryAttempts;
            BusyRetryAttempts = options.BusyRetryAttempts;
            BusyRetryDelayInSeconds = options.BusyRetryDelayInSeconds;
            CustomErrorDelayInSeconds = options.CustomErrorDelayInSeconds;
        }

        /// <summary>
        /// 設定錯誤重試
        /// </summary>
        /// <param name="attempts"></param>
        /// <returns></returns>
        public CustomAutoRetryOptions SetErrorRetry(int attempts)
        {
            ErrorRetryAttempts = attempts;
            return this;
        }
        /// <summary>
        /// 設定錯誤重試
        /// </summary>
        /// <param name="attempts"></param>
        /// <returns></returns>
        public CustomAutoRetryOptions SetErrorRetry(int attempts, int delayInSecond)
        {
            ErrorRetryAttempts = attempts;
            CustomErrorDelayInSeconds = delayInSecond;
            return this;
        }

        /// <summary>
        /// 設定閒置檢查重試
        /// </summary>
        /// <param name="attempts"></param>
        /// <param name="delayInSeconds"></param>
        /// <returns></returns>
        public CustomAutoRetryOptions SetBusyRetry(int attempts, int delayInSeconds)
        {
            BusyRetryAttempts = attempts;
            BusyRetryDelayInSeconds = delayInSeconds;
            return this;
        }
    }
}
