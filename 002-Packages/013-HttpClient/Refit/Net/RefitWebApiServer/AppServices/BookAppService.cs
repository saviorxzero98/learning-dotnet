using Refit;
using RefitWebApiCore.AppServices;
using RefitWebApiCore.Models;
using RefitWebApiCore.Models.Books;

namespace RefitWebApiServer.AppServices
{
    public class BookAppService : IBookAppService
    {
        /// <summary>
        /// 取得書籍
        /// </summary>
        /// <param name="bookId"></param>
        /// <returns></returns>
        public Task<GetBookResult> GetAsync(string bookId)
        {
            return Task.FromResult(new GetBookResult()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Alice's Adventures in Wonderland",
                Episode = 1,
                Price = 500,
                Author = new Author()
                {
                    Uid = 1,
                    Name = "Charles Lutwidge Dodgson"
                },
                PublishDate = new DateTime(1865, 7, 4),
                IsReprint = false
            });
        }

        /// <summary>
        /// 取得所有書籍
        /// </summary>
        /// <returns></returns>
        public Task<List<GetBookResult>> GetListAsync(PageDataQuery? query)
        {
            var results = new List<GetBookResult>()
            {
                new GetBookResult()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Alice's Adventures in Wonderland",
                    Episode = 1,
                    Price = 500,
                    Author = new Author()
                    {
                        Uid = 1,
                        Name = "Charles Lutwidge Dodgson"
                    },
                    PublishDate = new DateTime(1865, 7, 4),
                    IsReprint = false
                },
                new GetBookResult()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Cendrillon",
                    Episode = 6,
                    Price = 500,
                    Author = new Author()
                    {
                        Uid = 2,
                        Name = "Charles Perrault"
                    },
                    PublishDate = new DateTime(1697, 1, 4),
                    IsReprint = false
                },
                new GetBookResult()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Romeo and Juliet",
                    Episode = 1,
                    Price = 500,
                    Author = new Author()
                    {
                        Uid = 1,
                        Name = "William Shakespeare"
                    },
                    PublishDate = new DateTime(1595, 1, 1),
                    IsReprint = false
                },
                new GetBookResult()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "浦島伝説",
                    Episode = 1,
                    Price = 500,
                    Author = new Author(),
                    PublishDate = new DateTime(713, 1, 1),
                    IsReprint = false
                }
            };

            if (query != null)
            {
                var pagedResults = results.Skip(query.Offset)
                                          .Take(query.Limit)
                                          .ToList();
                return Task.FromResult(pagedResults);
            }
            else
            {
                return Task.FromResult(results);
            }
        }

        /// <summary>
        /// 新增書籍
        /// </summary>
        /// <param name="book"></param>
        /// <returns></returns>
        public Task<AddBookResult> AddAsync([Body] AddBookRequest book)
        {
            return Task.FromResult(new AddBookResult()
            {
                Id = Guid.NewGuid().ToString()
            });
        }

        /// <summary>
        /// 更新書籍
        /// </summary>
        /// <param name="bookId"></param>
        /// <param name="book"></param>
        /// <returns></returns>
        public Task UpdateAsync([AliasAs("id")] string bookId, [Body] UpdateBookRequest book)
        {
            return Task.CompletedTask;
        }

        /// <summary>
        /// 刪除書籍
        /// </summary>
        /// <param name="bookId"></param>
        /// <returns></returns>
        public Task DeleteAsync([AliasAs("id")] string bookId)
        {
            return Task.CompletedTask;
        }
    }
}
