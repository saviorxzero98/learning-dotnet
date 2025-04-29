using BackgroundJobServices;
using BackgroundWorkSample.Jobs;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackgroundWorkSample.Controllers
{
    [Route("api")]
    [ApiController]
    public class DemoBackgroundServiceController : ControllerBase
    {
        private readonly IBackgroundJobQueue _backgroundJobQueue;

        public DemoBackgroundServiceController(IBackgroundJobQueue backgroundJobQueue)
        {
            _backgroundJobQueue = backgroundJobQueue;
        }


        [HttpGet]
        [Route("BackgroundService")]
        public async Task<ActionResult> DemoAsync()
        {
            string message = "使用 Background Service";

            await _backgroundJobQueue.EnqueueAsync(new BigJob("BackgroundService", message, 10000));
            return Ok(message);
        }

        [HttpGet]
        [Route("BackgroundService/More")]
        public async Task<ActionResult> DemoMoreAsync()
        {
            string message = "使用 Background Service (More)";

            List<int> items = new List<int>();

            for (int i = 0; i < 5; i++)
            {
                await _backgroundJobQueue.EnqueueAsync(new BigJob($"BackgroundService_{i}", message, 10000));
            }
            return Ok(message);
        }
    }
}
