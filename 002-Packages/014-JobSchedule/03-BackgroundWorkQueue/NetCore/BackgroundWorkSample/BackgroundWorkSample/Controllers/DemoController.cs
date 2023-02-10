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

            await BigJob.ExecuteAsync("Sync", message, 10000);

            return Ok(message);
        }
    }
}
