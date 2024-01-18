using Microsoft.AspNetCore.Mvc;
using RefitWebApiCore.AppServices;
using RefitWebApiCore.Models;
using RefitWebApiCore.Models.Books;
using RefitWebApiServer.Models;

namespace RefitWebApiServer.Controllers
{
    [Route("api")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IBookAppService _appService;

        public BookController(IBookAppService appService,
                              IConfiguration configuration)
        {
            _appService = appService;
            _configuration = configuration;
        }

        [HttpGet]
        [Route("book/{id}")]
        public async Task<ActionResult> GetAsync(string id)
        {
            var result = await _appService.GetAsync(id);
            return HttpResponseResult.Ok(data: result);
        }


        [HttpGet]
        [Route("book")]
        public async Task<ActionResult> GetListAsync([FromQuery(Name = nameof(PageDataQuery.Limit))] string? limitText,
                                                     [FromQuery(Name = nameof(PageDataQuery.Offset))] string? offsetText)
        {
            if (int.TryParse(limitText, out int limit) &&
                int.TryParse(offsetText, out int offset))
            {
                var query = new PageDataQuery(offset, limit);
                var results = await _appService.GetListAsync(query);
                return HttpResponseResult.Ok(data: results);

            }
            else
            {
                var results = await _appService.GetListAsync(null);
                return HttpResponseResult.Ok(data: results);
            }
        }

        [HttpPost]
        [Route("book")]
        public async Task<ActionResult> AddAsync([FromBody] AddBookRequest book)
        {
            if (book == null)
            {
                return HttpResponseResult.BadRequest();
            }

            var result = await _appService.AddAsync(book);
            return HttpResponseResult.Ok(data: result);
        }

        [HttpPut]
        [Route("book/{id}")]
        public async Task<ActionResult> UpdateAsync(string id, [FromBody] UpdateBookRequest book)
        {
            if (book == null)
            {
                return HttpResponseResult.BadRequest();
            }

            await _appService.UpdateAsync(id, book);

            return HttpResponseResult.Ok();
        }

        [HttpDelete]
        [Route("book/{id}")]
        public async Task<ActionResult> DeleteAsync(string id)
        {
            await _appService.DeleteAsync(id);

            return HttpResponseResult.Ok();
        }
    }
}
