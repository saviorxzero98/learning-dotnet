using Microsoft.AspNetCore.Mvc;
using RefitWebApiCore.AppServices;
using RefitWebApiCore.Models;
using RefitWebApiCore.Models.Books;
using System.Net.Mime;

namespace RefitWebApiServer.Controllers
{
    [Route(IBookAppService.BasePath)]
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
        [Route(IBookAppService.ServiceNameWithId)]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<GetBookResult> GetAsync(string id)
        {
            var result = await _appService.GetAsync(id);
            return result;
        }


        [HttpGet]
        [Route(IBookAppService.ServiceName)]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<List<GetBookResult>> GetListAsync([FromQuery(Name = nameof(PageDataQuery.Limit))] string? limitText,
                                                            [FromQuery(Name = nameof(PageDataQuery.Offset))] string? offsetText)
        {
            if (int.TryParse(limitText, out int limit) &&
                int.TryParse(offsetText, out int offset))
            {
                var query = new PageDataQuery(offset, limit);
                var results = await _appService.GetListAsync(query);
                return results;

            }
            else
            {
                var results = await _appService.GetListAsync(null);
                return results;
            }
        }

        [HttpPost]
        [Route(IBookAppService.ServiceName)]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<AddBookResult> AddAsync([FromBody] AddBookRequest book)
        {
            var result = await _appService.AddAsync(book);
            return result;
        }

        [HttpPut]
        [Route(IBookAppService.ServiceNameWithId)]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task UpdateAsync(string id, [FromBody] UpdateBookRequest book)
        {
            await _appService.UpdateAsync(id, book);
        }

        [HttpDelete]
        [Route(IBookAppService.ServiceNameWithId)]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task DeleteAsync(string id)
        {
            await _appService.DeleteAsync(id);
        }
    }
}
