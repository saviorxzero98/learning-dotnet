using Quartz;
using Quartz.Impl;

namespace QuartzSample
{
    class Program
    {
        static void Main(string[] args)
        {
            // 建立排程器
            var schedulerFactory = new StdSchedulerFactory();
            var schedular = schedulerFactory.GetScheduler();

            // 建立Job
            IJobDetail job = JobBuilder.Create<SampleJob>()
                                       .WithIdentity("MyJob")
                                       .Build();

            // 建立Trigger
            ITrigger trigger = TriggerBuilder.Create()
                                             .WithCronSchedule("0/10 * * * * ?")
                                             .WithIdentity("MyJobTrigger")
                                             .Build();

            // Job配對Trigger
            schedular.ScheduleJob(job, trigger);

            // 啟動排程器
            schedular.Start();
        }
    }
}
