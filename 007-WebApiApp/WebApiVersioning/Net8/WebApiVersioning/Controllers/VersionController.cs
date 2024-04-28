using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace WebApiVersioning.Controllers
{
    [Route("api")]
    [ApiController]
    public class VersionController : ControllerBase
    {

        /// <summary>
        /// Get Version
        /// </summary>
        /// <returns></returns>
        /// GET api/version
        [HttpGet]
        [Route("v{version:apiVersion}/version")]
        [ApiVersion("1.0")]
        public ActionResult GetVersion()
        {
            return Ok("1.0");
        }

        /// <summary>
        /// Get Version
        /// </summary>
        /// <returns></returns>
        /// GET api/version
        [HttpGet]
        [Route("v{version:apiVersion}/version")]
        [ApiVersion("2.0")]
        public ActionResult GetVersion2()
        {
            return Ok("2.0");
        }
    }
}
