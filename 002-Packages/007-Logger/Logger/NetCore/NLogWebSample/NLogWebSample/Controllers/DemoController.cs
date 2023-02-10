using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NLog.Web;
using NLogWebSample.Services;
using System;

namespace NLogWebSample.Controllers
{
    [ApiController]
    [Route("api/demo")]
    public class DemoController : ControllerBase
    {
        private readonly ILoggerFactory _loggerFactory;
        private readonly ILogger _logger;
        private readonly CustomLogger _customLogger;

        public DemoController(ILoggerFactory loggerFactory)
        {
            _loggerFactory = loggerFactory;
            _logger = loggerFactory.CreateLogger<DemoController>();
            _customLogger = new CustomLogger();
        }

        [HttpGet]
        public ActionResult GetRequest()
        {
            _logger.LogInformation("GET api/demo");

            string errorMessage = "ServerTimeout: A timeout occurred during traversal evaluation of [RequestMessage{, requestId=01832523-4e47-47bb-8354-b1dc786d9517, op='bytecode', processor='traversal', args={gremlin=[[], [V(), hasLabel(drug), bothE(), drop(), none()]], aliases={g=rm}}}] - consider increasing the limit given to evaluationTimeout";
            Exception error = new Exception(errorMessage);
            _logger.LogError(error, errorMessage);
            _customLogger.Info("Demo", "GET api/demo (Part)");
            return Ok("OK");
        }

        private NLog.Logger GetLogger(string name)
        {
            return NLogBuilder.ConfigureNLog("NLog-Part.config").GetLogger(name);
        }
    }
}
