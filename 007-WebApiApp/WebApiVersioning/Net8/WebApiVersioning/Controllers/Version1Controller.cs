using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace WebApiVersioning.Controllers
{
    [Route("api")]
    [ApiController]
    [ApiVersion("1.0")]
    public class Version1Controller : ControllerBase
    {
        /// <summary>
        /// Get Version
        /// </summary>
        /// <returns></returns>
        /// GET api/version
        [HttpGet]
        [Route("version")]
        public ActionResult GetVersion()
        {
            return Ok("1.0");
        }
    }
}
