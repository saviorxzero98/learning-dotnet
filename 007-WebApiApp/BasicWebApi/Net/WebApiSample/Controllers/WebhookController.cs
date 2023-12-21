using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WebApiSample.Controllers
{
    /// <summary>
    /// Webhook
    /// </summary>
    [Route("api")]
    [ApiController]
    public class WebhookController : ControllerBase
    {
        /// <summary>
        /// GET Webhook
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("webhook")]
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
        public ActionResult Post([FromBody] JToken body)
        {
            string raw = JsonConvert.SerializeObject(body);

            return Ok();
        }
    }
}
