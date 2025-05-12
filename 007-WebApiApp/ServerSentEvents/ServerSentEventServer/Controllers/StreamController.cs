using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ServerSentEventServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StreamController : ControllerBase
    {
        [HttpGet]
        public async Task GetAsync()
        {
            var id = Guid.NewGuid().ToString();
            var chunks = new List<string>()
            {
                "哈",
                "囉",
                "!",
                "小",
                "歐"
            };

            Response.ContentType = "text/event-stream";

            foreach (var chunk in chunks)
            {
                var json = JsonConvert.SerializeObject(new
                {
                    id = id,
                    choices = new[]
                    {
                        new
                        {
                            delta = new { content = chunk },
                            index = 0,
                            finish_reason = (string?)null
                        }
                    }
                });

                var data = $"data: {json}\n\n";

                await Response.WriteAsync($"data: {data}");
                await Response.Body.FlushAsync();
                await Task.Delay(2000);
            }

            await Response.WriteAsync($"data: [DONE]\n\n");
            await Response.Body.FlushAsync();
        }



        [HttpPost]
        public async Task PostAsync()
        {
            var id = Guid.NewGuid().ToString();
            var chunks = new List<string>()
            {
                "哈",
                "囉",
                "!",
                "小",
                "歐"
            };

            Response.ContentType = "text/event-stream";

            foreach (var chunk in chunks)
            {
                var json = JsonConvert.SerializeObject(new
                {
                    id = id,
                    choices = new[]
                    {
                        new
                        {
                            delta = new { content = chunk },
                            index = 0,
                            finish_reason = (string?)null
                        }
                    }
                });

                var data = $"data: {json}\n\n";

                await Response.WriteAsync($"data: {data}");
                await Response.Body.FlushAsync();
                await Task.Delay(2000);
            }

            await Response.WriteAsync($"data: [DONE]\n\n");
            await Response.Body.FlushAsync();
        }
    }
}
