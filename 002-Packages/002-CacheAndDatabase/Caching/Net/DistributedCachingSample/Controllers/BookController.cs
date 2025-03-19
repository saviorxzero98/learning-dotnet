using DistributedCachingSample.Models;
using Microsoft.AspNetCore.Mvc;
using ZiggyCreatures.Caching.Fusion;

namespace DistributedCachingSample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookController : ControllerBase
    {
        private readonly ILogger<BookController> _logger;
        private readonly IFusionCache _cache;

        public BookController(
            ILogger<BookController> logger,
            IFusionCache cache)
        {
            _logger = logger;
            _cache = cache;
        }

        [HttpGet]
        public async Task<BookResponse> GetAsync(string id)
        {
            var book = await _cache.GetOrDefaultAsync<Book>(GetCacheKey(id));
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
                var book = await _cache.GetOrDefaultAsync<Book>(bookId);
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

            var tags = GetTags(book);
            await _cache.SetAsync(GetCacheKey(book.Id), book, tags: tags);
            return book;
        }

        [HttpDelete]
        [Route("byTags")]
        public async Task DeleteByTagAsync([FromBody] RefreshBookTagRequest refresh)
        {
            if (refresh == null || refresh.Tags == null || refresh.Tags.Count == 0)
            {
                throw new ArgumentNullException(nameof(refresh));
            }

            await _cache.RemoveByTagAsync(refresh.Tags);
        }


        [HttpDelete]
        [Route("{id}")]
        public async Task DeleteAsync(string id)
        {
            await _cache.RemoveByTagAsync(GetCacheKey(id));
        }


        private string GetCacheKey(string id)
        {
            return $"Library:Default:{id}";
        }

        private List<string> GetTags(Book book)
        {
            var tags = new List<string>();

            if (book == null)
            {
                return tags;
            }

            if (!string.IsNullOrWhiteSpace(book.Author))
            {
                tags.Add($"Book:Author:{book.Author}");
            }

            if (!string.IsNullOrWhiteSpace(book.Publisher))
            {
                tags.Add($"Book:Publisher:{book.Publisher}");
            }

            if (book.Categories.Any(i => !string.IsNullOrWhiteSpace(i)))
            {
                tags.AddRange(book.Categories.Select(cat => $"Book:Category:{cat}"));
            }
            return tags;
        }
    }
}
