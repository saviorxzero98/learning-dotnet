using MediatAlt;
using MediatAltSample.Events;
using Microsoft.AspNetCore.Mvc;

namespace MediatAltSample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DemoController : ControllerBase
    {
        private readonly ILogger<DemoController> _logger;
        private readonly IMediator _mediator;

        public DemoController(
            ILogger<DemoController> logger,
            IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<string> GetAsync()
        {
            var readEvent = new ReadDemoEvent();
            var response = await _mediator.SendAsync<ReadDemoEvent, string>(readEvent);
            return response;
        }

        [HttpPost]
        public async Task AddAsync()
        {
            var addEvent = new AddDemoEvent() 
            {
                Id = 1,
                Name = "First" 
            };
            await _mediator.PublishAsync(addEvent);
        }
    }
}
