using Hangfire;
using HangfireCustomAutoRetrySample.Exceptions;
using HangfireCustomAutoRetrySample.JobFilters;
using HangfireCustomAutoRetrySample.Toolkits;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HangfireCustomAutoRetrySample.Jobs
{
    public class DemoJob
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="retryOptions">錯誤重試、服務忙碌重試的次數和間隔時間設定，僅提供自訂 Retry</param>
        /// <returns></returns>
        [CustomAutoRetry()]             // 自訂 Retry 處理
        [AutomaticRetry(Attempts = 0)]  // 關閉原生的 Retry 處理
        public Task DoErrorThing(CustomAutoRetryOptions retryOptions)
        {
            var random = new RandomGenerator();
            int number = random.Next(3);

            switch (number)
            {
                case 2:
                    throw new BusyServiceException();
                case 1:
                    throw new Exception("Error");
                default:
                    return Task.CompletedTask;
            }
        }

        [CustomAutoRetry(BusyRetryAttempts = 2, BusyRetryDelayInSeconds = 2)]     // 自訂 Retry 處理
        [AutomaticRetry(Attempts = 0)]                                            // 關閉原生的 Retry 處理
        public Task DoBusyThing()
        {
            // TODO: Do SomeThing

            // 透過 Throw Exception 來觸發 Job Retry
            throw new BusyServiceException();
        }

        public Task DoSomeThing()
        {
            Thread.Sleep(5000);     // 停留 5秒

            return Task.CompletedTask;
        }
    }
}
