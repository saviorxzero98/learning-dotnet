using BackgroundJobServices;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace BackgroundWorkSample.Jobs
{
    public class BigJob : IBackgroundJob
    {
        private readonly string _jobId;
        private readonly string _message;
        private readonly int _delayTimes;


        public BigJob(string jobId, string message, int delayTimes = 5000)
        {
            _delayTimes = delayTimes;
            _jobId = jobId;
            _message = message;
        }

        public Task ExecuteAsync(CancellationToken token = default)
        {
            // 留下執行紀錄
            using (StreamWriter sw = new StreamWriter($"Results/{_jobId}-Background.txt", true))
            {
                DateTime now = DateTime.Now;
                sw.WriteLine($"[{now.ToString("yyyy-MM-dd HH:mm:ss")}] {_message} (Start)");
            }

            // 停留 10秒
            Thread.Sleep(_delayTimes);

            // 留下執行紀錄
            using (StreamWriter sw = new StreamWriter($"Results/{_jobId}-Background.txt", true))
            {
                DateTime now = DateTime.Now;
                sw.WriteLine($"[{now.ToString("yyyy-MM-dd HH:mm:ss")}] {_message} (Done)");
            }

            return Task.CompletedTask;
        }

        public Task OnExceptionAsync(Exception exception, CancellationToken token = default)
        {
            return Task.CompletedTask;
        }
    }
}
