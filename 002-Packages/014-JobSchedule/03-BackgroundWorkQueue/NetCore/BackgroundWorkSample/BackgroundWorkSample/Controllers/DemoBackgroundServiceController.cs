using BackgroundWorkSample.Jobs;
using BackgroundWorkSample.BackgroundServices;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using BackgroundWorkSample.Extensions;
using System.Threading.Tasks;

namespace BackgroundWorkSample.Controllers
{
    [Route("api")]
    [ApiController]
    public class DemoBackgroundServiceController : ControllerBase
    {
        private readonly BackgroundJobQueue _backgroundQueue;

        public DemoBackgroundServiceController(BackgroundJobQueue backgroundQueue)
        {
            _backgroundQueue = backgroundQueue;
        }


        [HttpGet]
        [Route("BackgroundService")]
        public ActionResult DemoAsync()
        {
            string message = "使用 Background Service";

            _backgroundQueue.Enqueue(async (cancellationToken) =>
            {
                await BigJob.ExecuteAsync("BackgroundService", message, 10000);
            });

            return Ok(message);
        }

        [HttpGet]
        [Route("BackgroundService/More")]
        public ActionResult DemoMoreAsync()
        {
            string message = "使用 Background Service (More)";

            _backgroundQueue.Enqueue(async (cancellationToken) =>
            {
                List<int> items = new List<int>();

                for (int i = 0; i < 5; i++)
                {
                    items.Add(i+1);
                }

                await items.ParallelForEachAsync(async (item) =>
                {
                    await BigJob.ExecuteAsync($"BackgroundService_{item}", message, 10000);
                });
            });

            return Ok(message);
        }
    }
}
