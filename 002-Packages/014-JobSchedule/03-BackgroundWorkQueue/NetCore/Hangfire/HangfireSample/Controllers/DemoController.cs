using Hangfire;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace HangfireSample.Controllers
{
    [Route("api")]
    [ApiController]
    public class DemoController : ControllerBase
    {
        [HttpGet]
        [Route("DemoAsync")]
        public ActionResult DemoAsync()
        {
            string message = "已使用 Background Queue";

            // 非同步處理 Request
            BackgroundJob.Enqueue(() => DoBigWork(message));

            return Ok(message);
        }

        [HttpGet]
        [Route("DemoSync")]
        public async Task<ActionResult> DemoSync()
        {
            string message = "未使用 Background Queue";

            await DoBigWork(message);

            return Ok(message);
        }

        /// <summary>
        /// 工作
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        [NonAction]
        public Task DoBigWork(string message)
        {
            // 停留 10秒
            Thread.Sleep(10000);

            // 留下執行紀錄
            using (StreamWriter sw = new StreamWriter("Background.txt", true))
            {
                DateTime now = DateTime.Now;
                sw.WriteLine($"[{now.ToString("yyyy-MM-dd HH:mm:ss")}] {message} (Done)");
            }

            return Task.CompletedTask;
        }
    }
}
