using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WebApiSample.Controllers
{
    [Route("api")]
    [ApiController]
    public class EtaDemoController : ControllerBase
    {
        [HttpGet]
        [Route("etl/webhook")]
        public ActionResult Get()
        {
            return Ok();
        }

        [HttpPost]
        [Route("etl/webhook")]
        public ActionResult Post([FromBody] JToken body)
        {
            string raw = JsonConvert.SerializeObject(body);

            return Ok();
        }
    }
}
