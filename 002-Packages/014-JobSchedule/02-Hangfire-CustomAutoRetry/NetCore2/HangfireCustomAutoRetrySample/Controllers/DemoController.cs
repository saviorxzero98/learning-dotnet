using Hangfire;
using HangfireCustomAutoRetrySample.JobFilters;
using HangfireCustomAutoRetrySample.Jobs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HangfireCustomAutoRetrySample.Controllers
{
    [Route("api")]
    [ApiController]
    public class DemoController : ControllerBase
    {
        [HttpGet]
        [Route("demo")]
        public ActionResult Enqueue()
        {
            BackgroundJob.Enqueue<DemoJob>(job => job.DoSomeThing());

            return Ok("OK");
        }

        [HttpGet]
        [Route("demo/error")]
        public ActionResult EnqueueError()
        {
            // 非同步處理 Request
            var retryOptions = new CustomAutoRetryOptions();

            // 設定錯誤重試、服務忙碌重試的次數和間隔時間
            int errorRetryAttempts = 5;
            int errorRetryDelayInSeconds = 3;
            int busyRetryAttempts = 5;
            int busyRetryDelayInSeconds = 3;
            retryOptions = retryOptions.SetErrorRetry(errorRetryAttempts, errorRetryDelayInSeconds)
                                       .SetBusyRetry(busyRetryAttempts, busyRetryDelayInSeconds);

            // 錯誤重試、服務忙碌重試的次數和間隔時間的設定需要傳入
            BackgroundJob.Enqueue<DemoJob>(job => job.DoErrorThing(retryOptions));

            return Ok("OK");
        }

        [HttpGet]
        [Route("demo/busy")]
        public ActionResult EnqueueBusyError()
        {
            // 非同步處理 Request
            var retryOptions = new CustomAutoRetryOptions();
            retryOptions.SetBusyRetry(5, 3);

            // 錯誤重試、服務忙碌重試的次數和間隔時間的設定需要傳入
            BackgroundJob.Enqueue<DemoJob>(job => job.DoBusyThing());

            return Ok("OK");
        }
    }
}
