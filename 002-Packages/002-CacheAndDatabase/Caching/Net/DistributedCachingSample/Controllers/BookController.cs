using DistributedCachingSample.Models;
using DistributedCachingSample.Services;
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
        private readonly IDistributedCacheManager<Book> _cacheManager;

        public BookController(
            ILogger<BookController> logger,
            IDistributedCache cache)
        {
            _logger = logger;
            _cache = cache;
            _cacheManager = new MemoryCacheManager<Book>(cache);
        }

        [HttpGet]
        public async Task<BookResponse> GetAsync(string id)
        {
            var book = await _cacheManager.GetAsync(GetCacheKey(id));

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
            var books = await _cacheManager.GetManyAsync(bookIds);
            return new BookResponse(books);
        }

        [HttpPost]
        public async Task<Book?> AddAsync([FromBody] Book book)
        {
            if (book == null || string.IsNullOrEmpty(book.Id))
            {
                throw new ArgumentNullException(nameof(book));
            }

            var cacheData = await _cacheManager.SetAsync(GetCacheKey(book.Id), book);
            return cacheData;
        }
    
        private string GetCacheKey(string id)
        {
            return $"Library:Default:{id}";
        }
    }
}
