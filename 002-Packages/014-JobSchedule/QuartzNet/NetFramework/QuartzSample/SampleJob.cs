using Quartz;
using System;

namespace QuartzSample
{
    [DisallowConcurrentExecution]
    public class SampleJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            var message = DemoCronExpression();
            Console.WriteLine($"[{DateTime.Now.ToString("HH:mm:ss")}] 完成工作，{message}");
        }

        public string DemoCronExpression()
        {
            var message = string.Empty;
            var cron = new CronExpression("* * * ? * SUN,SAT");

            if (cron.IsSatisfiedBy(DateTime.Today))
            {
                message += "今天是例假日";
            }
            else
            {
                message += "今天不是例假日";
            }

            var cron2 = new CronExpression("* * * ? * MON-FRI");

            if (cron2.IsSatisfiedBy(DateTime.Today))
            {
                message += "，是工作日";
            }
            else
            {
                message += "今，工作日";
            }

            return message;
        }
    }
}
