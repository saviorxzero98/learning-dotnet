using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WebApiSample.Controllers
{
    /// <summary>
    /// Webhook
    /// </summary>
    [ApiController]
    //[Route("api")]
    [Route("api")]
    public class WebhookController : ControllerBase
    {
        /// <summary>
        /// GET Webhook
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("webhook")]
        [ApiVersion("1.0")]
        public ActionResult Get()
        {
            return Ok();
        }


        /// <summary>
        /// POST Webhook
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("webhook")]
        [ApiVersion("2.0")]
        public ActionResult Post([FromBody] JToken body)
        {
            string raw = JsonConvert.SerializeObject(body);

            return Ok();
        }
    }
}
