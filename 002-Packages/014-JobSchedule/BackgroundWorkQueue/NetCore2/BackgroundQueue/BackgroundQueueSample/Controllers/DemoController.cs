using DalSoft.Hosting.BackgroundQueue;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace BackgroundQueueSample.Controllers
{
    [Route("api")]
    [ApiController]
    public class DemoController : ControllerBase
    {
        private readonly BackgroundQueue _backgroundQueue;

        public DemoController(BackgroundQueue backgroundQueue)
        {
            _backgroundQueue = backgroundQueue;
        }

        [HttpGet]
        [Route("DemoAsync")]
        public ActionResult DemoAsync()
        {
            string message = "已使用 Background Queue";

            // 非同步處理 Request
            _backgroundQueue.Enqueue(async cancellationToken =>
            {
                await DoBigWork(message);
            });

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
        private Task DoBigWork(string message)
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
