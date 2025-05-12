using Microsoft.AspNetCore.Mvc;

namespace ServerSentEventServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DemoController : ControllerBase
    {

        [HttpGet]
        public string Get()
        {
            return "Hello";
        }
    }
}
