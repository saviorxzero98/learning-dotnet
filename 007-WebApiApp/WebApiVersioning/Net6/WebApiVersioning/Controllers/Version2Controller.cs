using Microsoft.AspNetCore.Mvc;

namespace WebApiVersioning.Controllers
{
    [Route("api")]
    [ApiController]
    [ApiVersion("2.0")]
    public class Version2Controller : ControllerBase
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
            return Ok("2.0");
        }
    }
}
