using DistributedCachingSample.Extensions;
using DistributedCachingSample.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

namespace DistributedCachingSample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookController : ControllerBase
    {
        private readonly ILogger<BookController> _logger;
        private readonly IDistributedCache _cache;

        public BookController(
            ILogger<BookController> logger,
            IDistributedCache cache)
        {
            _logger = logger;
            _cache = cache;
        }

        [HttpGet]
        public async Task<BookResponse> GetAsync(string id)
        {
            var book = await _cache.GetDataOrDefaultAsync<Book>(GetCacheKey(id));
            return new BookResponse(book);
        }

        [HttpPost]
        [Route("search")]
        public async Task<BookResponse> SearchAsync([FromBody] SearchBookRequest searchDto)
        {
            if (searchDto == null)
            {
                throw new ArgumentNullException(nameof(searchDto));
            }

            var bookIds = searchDto.BookIds.Select(i => GetCacheKey(i)).ToList();

            var books = new List<Book>();
            foreach (var bookId in bookIds)
            {
                var book = await _cache.GetDataOrDefaultAsync<Book>(bookId);
                if (book != null)
                {
                    books.Add(book);
                }
            }
            return new BookResponse(books);
        }

        [HttpPost]
        public async ValueTask<Book> AddAsync([FromBody] Book book)
        {
            if (book == null || string.IsNullOrEmpty(book.Id))
            {
                throw new ArgumentNullException(nameof(book));
            }

            await _cache.SetDataAsync(GetCacheKey(book.Id), book);
            return book;
        }


        [HttpDelete]
        [Route("{id}")]
        public async Task DeleteAsync(string id)
        {
            await _cache.RemoveAsync(GetCacheKey(id));
        }


        private string GetCacheKey(string id)
        {
            return $"Library:Default:{id}";
        }
    }
}
