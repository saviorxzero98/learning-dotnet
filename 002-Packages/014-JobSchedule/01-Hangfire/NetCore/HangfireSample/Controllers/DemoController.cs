using Hangfire;
using Hangfire.Console;
using Hangfire.Server;
using Hangfire.States;
using Hangfire.Storage;
using HangfireSample.Exceptions;
using HangfireSample.Extensions;
using HangfireSample.JobFilters;
using HangfireSample.Jobs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HangfireSample.Controllers
{
    [Route("api")]
    [ApiController]
    public class DemoController : ControllerBase
    {
        public IConfiguration _config;

        public DemoController(IConfiguration config)
        {
            _config = config;
        }


        [HttpGet]
        [Route("Hangfire/Enqueue")]
        public ActionResult Enqueue()
        {
            // 非同步處理 Request
            var jobId = BackgroundJob.Enqueue(() => DoSomeThing(null));
            var jobId2 = BackgroundJob.Enqueue(() => DoSomeThing2(null));
            
            return Ok("已使用 Hangfire (Fire-and-forget tasks)");
        }

        [HttpGet]
        [Route("Hangfire/Schedule")]
        public ActionResult Schedule()
        {
            // 排程處理 Request
            var jobId = BackgroundJob.Schedule(() => DoSomeThing(null), TimeSpan.FromSeconds(10));
            var jobId2 = BackgroundJob.Schedule(() => DoSomeThing2(null), TimeSpan.FromSeconds(5));

            return Ok("已使用 Hangfire (Delayed tasks)");
        }

        [HttpGet]
        [Route("NoHangfire")]
        public async Task<ActionResult> NoSchedule()
        {
            await DoSomeThing(null);

            return Ok("未使用 Hangfire");
        }

        [HttpGet]
        [Route("Hangfire/Recurring")]
        public ActionResult Recurring()
        {
            RecurringJob.RemoveIfExists("0001");
            var retryOptions = new SingleJobOptions() { JobId = "ABC" } ;
            RecurringJob.AddOrUpdate<CustomJob>("0001", j => j.ExecuteJobAsync("Custom", retryOptions, null, null), Cron.Hourly);

            var retryOptions2 = new SingleJobOptions() { JobId = "DEF" };
            RecurringJob.AddOrUpdate<CustomJob>("0002", j => j.ExecuteJobAsync("Custom2", retryOptions2, null, null), Cron.Hourly);

            return Ok("Add Recurring Success");
        }

        [HttpGet]
        [Route("Hangfire/Recurring/Delete")]
        public ActionResult RemoveRecurring()
        {
            RecurringJob.RemoveIfExists("0001");
            return Ok("Remove Recurring Success");
        }

        [HttpGet]
        [Route("Hangfire/Recurring/DeleteAndCancelLast")]
        public ActionResult RemoveAndCancelRecurring()
        {
            string jobId = "0001";

            try
            {
                List<RecurringJobDto> list;
                using (IStorageConnection connection = JobStorage.Current.GetConnection())
                {
                    list = connection.GetRecurringJobs();
                }

                RecurringJobDto job = list?.FirstOrDefault(j => j.Id == jobId);
                if (job != null && !string.IsNullOrEmpty(job.LastJobId))
                {
                    BackgroundJob.Delete(job.LastJobId);
                }
            }
            catch
            {

            }

            RecurringJob.RemoveIfExists(jobId);
            return Ok("Cancel Recurring Last Job Success");
        }

        [HttpGet]
        [Route("Hangfire/Recurring/CancelLast")]
        public ActionResult CancelRecurring()
        {
            string jobId = "0001";

            try
            {
                List<RecurringJobDto> list;
                using (IStorageConnection connection = JobStorage.Current.GetConnection())
                {
                    list = connection.GetRecurringJobs();
                }

                RecurringJobDto job = list?.FirstOrDefault(j => j.Id == jobId);
                if (job != null && !string.IsNullOrEmpty(job.LastJobId))
                {
                    string lastJobState = job.LastJobState;

                    if (lastJobState == DeletedState.StateName ||
                        lastJobState == SucceededState.StateName ||
                        lastJobState == FailedState.StateName)
                    {
                        return Ok("Remove Recurring And Cancel Last Job Success");
                    }
                    BackgroundJob.Delete(job.LastJobId);
                }
            }
            catch
            {

            }
            return Ok("Remove Recurring And Cancel Last Job Success");
        }


        [HttpGet]
        [Route("Hangfire/EnqueueError")]
        public ActionResult EnqueueError()
        {
            // 非同步處理 Request
            var retryOptions = new CustomAutoRetryOptions().SetErrorRetry(5, 3)
                                                           .SetIdleCheckRetry(5, 3);
            BackgroundJob.Enqueue(() => DoErrorThing(retryOptions, null));

            return Ok("OK");
        }

        public Task DoSomeThing(PerformContext context)
        {
            context.WriteLine("開始 Do Some Thing");

            ILogger logger = new HangfireConsoleLogger(context);
            logger.LogTrace("Trace Log");
            logger.LogDebug("Debug Log");
            logger.LogInformation("Infomation Log");
            logger.LogWarning("Warning Log");
            logger.LogError("Error Log");
            logger.LogCritical("Critical Log");

            // 停留 10秒
            var progress = context.WriteProgressBar();
            for (int i = 1; i <= 100; i++)
            {
                progress.SetValue(i);

                Thread.Sleep(500);
            }

            context.WriteLine("完成 Do Some Thing");

            return Task.CompletedTask;
        }

        [Queue("custom")]
        public Task DoSomeThing2(PerformContext context)
        {
            context.WriteLine("開始執行 Do Some Thing");

            // 停留 10秒
            var progress = context.WriteProgressBar();
            for (int i = 1; i <= 100; i++)
            {
                progress.SetValue(i);

                Thread.Sleep(100);
            }

            context.WriteLine("完成 End Do Some Thing");

            return Task.CompletedTask;
        }

        [Queue("custom")]
        public Task DoSomeThing3(PerformContext context)
        {
            return Task.CompletedTask;
        }

        [CustomAutoRetry()]
        [AutomaticRetry(Attempts = 0)]
        private Task DoErrorThing(CustomAutoRetryOptions retryOptions, PerformContext context)
        {
            var random = new Random();
            int number = random.Next(2);

            if (number > 0)
            {
                context.WriteLine("Busy Bot Exception");
                throw new BusyBotException();
            }
            else
            {
                context.WriteLine("Error");
                throw new Exception("Error");
            }
        }
    }
}
