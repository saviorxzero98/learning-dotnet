using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace WebApiVersioning.Controllers
{
    [Route("api")]
    [ApiController]
    public class HealthCheckController : ControllerBase
    {
        /// <summary>
        /// Get Health Check
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("HealthCheck")]
        [ApiVersionNeutral]
        public ActionResult Get()
        {
            return Ok("OK");
        }

        /// <summary>
        /// Get Health Check
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("HealthCheckOld")]
        [ApiVersion("1.0", Deprecated = true)]
        public ActionResult GetOld()
        {
            return Ok("OK");
        }
    }
}
