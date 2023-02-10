using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace BackgroundWorkSample.Jobs
{
    public class BigJob
    {
        public static Task ExecuteAsync(string jobId, string message, int delayTimes = 5000)
        {
            // 留下執行紀錄
            using (StreamWriter sw = new StreamWriter($"Results/{jobId}-Background.txt", true))
            {
                DateTime now = DateTime.Now;
                sw.WriteLine($"[{now.ToString("yyyy-MM-dd HH:mm:ss")}] {message} (Start)");
            }

            // 停留 10秒
            Thread.Sleep(delayTimes);

            // 留下執行紀錄
            using (StreamWriter sw = new StreamWriter($"Results/{jobId}-Background.txt", true))
            {
                DateTime now = DateTime.Now;
                sw.WriteLine($"[{now.ToString("yyyy-MM-dd HH:mm:ss")}] {message} (Done)");
            }

            return Task.CompletedTask;
        }
    }
}
