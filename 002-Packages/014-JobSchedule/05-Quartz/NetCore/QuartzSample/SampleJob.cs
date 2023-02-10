using Quartz;
using System;
using System.Threading.Tasks;

namespace QuartzSample
{
    [DisallowConcurrentExecution]
    public class SampleJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            var message = DemoCronExpression();
            Console.WriteLine($"[{DateTime.Now.ToString("HH:mm:ss")}] 完成工作，{message}");
        }

        public string DemoCronExpression()
        {
            var message = string.Empty;

            var cron = new CronExpression("* * * ? * SUN,SAT");
            var cron2 = new CronExpression("* * * ? * MON-FRI");

            var nextCron = new CronExpression("1 1 0 ? * SUN,SAT");
            var nextCron2 = new CronExpression("1 1 0 ? * MON-FRI");

            var nextCronDateTime = (DateTimeOffset)cron.GetNextValidTimeAfter(DateTime.Now);
            var nextCron2DateTime = (DateTimeOffset)cron2.GetNextValidTimeAfter(DateTime.Now);

            if (cron.IsSatisfiedBy(DateTime.Today))
            {
                message += $"今天是例假日，下一個工作日是在 {nextCron2DateTime.ToLocalTime().ToString("yyyy-MM-dd")}";
            }
            if (cron2.IsSatisfiedBy(DateTime.Today))
            {
                message += $"今天不是例假日，下一個例假日是在 {nextCronDateTime.ToLocalTime().ToString("yyyy-MM-dd")}";
            }

            return message;
        }
    }
}
