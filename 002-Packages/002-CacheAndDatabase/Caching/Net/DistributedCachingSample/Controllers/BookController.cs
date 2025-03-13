using DistributedCachingSample.Caching;
using DistributedCachingSample.Models;
using Microsoft.AspNetCore.Mvc;

namespace DistributedCachingSample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookController : ControllerBase
    {
        private readonly ILogger<BookController> _logger;
        private readonly ICacheManager _cacheManager;

        public BookController(
            ILogger<BookController> logger,
            ICacheManager cacheManager)
        {
            _logger = logger;
            _cacheManager = cacheManager;
        }

        [HttpGet]
        public async Task<BookResponse> GetAsync(string id)
        {
            var book = await _cacheManager.GetOrCreateAsync(GetCacheKey(id),
                                                            (token) => ValueTask.FromResult(default(Book)));
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
                var book = await _cacheManager.GetOrCreateAsync(bookId,
                                                                (token) => ValueTask.FromResult(default(Book)));
                if (book != null)
                {
                    books.Add(book);
                }
            }
            return new BookResponse(books);
        }

        [HttpPost]
        public ValueTask<Book> AddAsync([FromBody] Book book)
        {
            if (book == null || string.IsNullOrEmpty(book.Id))
            {
                throw new ArgumentNullException(nameof(book));
            }

            var tags = GetTags(book);
            _cacheManager.SetAsync(GetCacheKey(book.Id), book, tags: tags);
            return ValueTask.FromResult(book);
        }

        [HttpDelete]
        [Route("byTags")]
        public async Task DeleteByTagAsync([FromBody] RefreshBookTagRequest refresh)
        {
            if (refresh == null || refresh.Tags == null || refresh.Tags.Count == 0)
            {
                throw new ArgumentNullException(nameof(refresh));
            }

            await _cacheManager.RemoveByTagAsync(refresh.Tags);
        }


        [HttpDelete]
        [Route("{id}")]
        public async Task DeleteAsync(string id)
        {
            await _cacheManager.RemoveAsync(GetCacheKey(id));
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
