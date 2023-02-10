using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using WebApiSample.Models;
using WebApiSample.Services;

namespace WebApiSample.Controllers
{
    [ApiController]
    [Route("api")]
    public class DemoController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IIdentifierGenerator _idGenerator;

        public DemoController(IConfiguration configuration, IIdentifierGenerator idGenerator)
        {
            _configuration = configuration;
            _idGenerator = idGenerator;
        }

        [HttpGet]
        [Route("version")]
        public ActionResult GetVersion()
        {
            var version = _configuration.GetSection("Version").Value.ToString();
            return Ok(version);
        }

        [HttpGet]
        [Route("newid")]
        public ActionResult GetNewId()
        {
            string result = _idGenerator.Create();
            return Ok(result);
        }

        [HttpPost]
        [Route("circle")]
        public ActionResult CalcCircleArea([FromBody] CircleInfo info)
        {
            CircleInfo result = new CircleInfo()
            {
                Radius = info.Radius,
                Area = info.Radius * info.Radius * Math.PI,
                Circumference = info.Radius * 2 * Math.PI
            };
            return Ok(result);
        }
    }
}
