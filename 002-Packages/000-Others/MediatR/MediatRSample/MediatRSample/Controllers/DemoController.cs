using MediatR;
using MediatRSample.Events;
using Microsoft.AspNetCore.Mvc;

namespace MediatRSample.Controllers
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
            var response = await _mediator.Send(new ReadDemoEvent());
            return response;
        }

        [HttpPost]
        public Task AddAsync()
        {
            _mediator.Publish(new AddDemoEvent() {  Id = 1, Name = "First" });
            return Task.CompletedTask;
        }
    }
}
