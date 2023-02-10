using Microsoft.AspNetCore.Mvc;

namespace FluentMigrationsWebSample.Controllers
{
    [Route("api")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/Demo
        [HttpGet("Demo")]
        public ActionResult Get()
        {
            return Ok("Success");
        }
    }
}
