using Microsoft.AspNetCore.Mvc;
using System;

namespace WebApplicationCatchUnhandledException.Controllers
{
    [Route("api")]
    [ApiController]
    public class DemoController : ControllerBase
    {
        // GET api/demo/filter
        [HttpGet]
        [Route("demo/filter")]
        public ActionResult<string> DemoFilter()
        {
            throw new NotImplementedException("Error WebApi");
        }

        // GET api/demo/middleware
        [HttpGet]
        [Route("demo/middleware")]
        public ActionResult<string> DemoMiddleware()
        {
            return "Success";
        }
    }
}
