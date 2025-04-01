using FusionCacheSample.Factories;
using FusionCacheSample.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using ZiggyCreatures.Caching.Fusion;

namespace FusionCacheSample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookController
    {
        private readonly ILogger<BookController> _logger;
        private readonly IFusionCache _fusionCache;
        private readonly IDistributedCache _distributedCache;

        public BookController(
            ILogger<BookController> logger,
            IFusionCacheFactory cacheFactory,
            IDistributedCache distributedCache)
        {
            _logger = logger;
            _fusionCache = cacheFactory.GetFusionCache();
            _distributedCache = distributedCache;
        }

        [HttpGet]
        public async Task<BookResponse> GetAsync(string id)
        {
            var book = await _fusionCache.GetOrDefaultAsync<Book>(GetCacheKey(id));
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
                var book = await _fusionCache.GetOrDefaultAsync<Book>(bookId);
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
            await _fusionCache.SetAsync(GetCacheKey(book.Id), book, tags: tags);
            await _distributedCache.SetStringAsync(($"T:{book.Id}"), JsonConvert.SerializeObject(book));

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

            await _fusionCache.RemoveByTagAsync(refresh.Tags);
        }


        [HttpDelete]
        [Route("{id}")]
        public async Task DeleteAsync(string id)
        {
            await _fusionCache.RemoveAsync(GetCacheKey(id));
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
