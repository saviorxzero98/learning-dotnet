using BackgroundWorkSample.Jobs;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BackgroundWorkSample.Controllers
{
    [Route("api")]
    [ApiController]
    public class DemoController : ControllerBase
    {
        [HttpGet]
        [Route("Demo")]
        public async Task<ActionResult> DemoAsync()
        {
            string message = "執行 Big Job";

            var job = new BigJob("Sync", message, 10000);
            await job.ExecuteAsync();

            return Ok(message);
        }
    }
}
