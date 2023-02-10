using Quartz;
using Quartz.Impl;
using System;
using System.Threading.Tasks;

namespace QuartzSample
{
    class Program
    {
        static void Main(string[] args)
        {
            var SampleJob = new SampleJob();
            Console.WriteLine(SampleJob.DemoCronExpression());

            //Task.Run(Start);

            Console.ReadLine();
        }

        static async Task Start()
        {
            // 建立排程器
            var schedulerFactory = new StdSchedulerFactory();
            var schedular = await schedulerFactory.GetScheduler();

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
            await schedular.ScheduleJob(job, trigger);

            // 啟動排程器
            await schedular.Start();
        }
    }
}
