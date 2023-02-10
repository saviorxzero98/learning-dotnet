using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Hosting;
using System.Web.Http;

namespace QueueBackgroundWorkItemSample.Controllers
{
    [RoutePrefix("api")]
    public class DemoController : ApiController
    {
        [HttpGet]
        [Route("DemoAsync")]
        // GET api/DemoAsync
        public async Task<HttpResponseMessage> DemoAsync()
        {
            HostingEnvironment.QueueBackgroundWorkItem(async cancellationToken =>
            {
                await DoSomeThing();
            });

            return Request.CreateResponse(HttpStatusCode.OK, new
            {
                Message = "已使用 Queue Background Work Item"
            });
        }

        [HttpGet]
        [Route("DemoSync")]
        // GET api/DemoAsync
        public async Task<HttpResponseMessage> DemoSync()
        {
            await DoSomeThing();

            return Request.CreateResponse(HttpStatusCode.OK, new
            {
                Message = "未使用 Queue Background Work Item"
            });
        }

        private async Task DoSomeThing()
        {
            // 停留 10秒
            Thread.Sleep(10000);
        }
    }
}