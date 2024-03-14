using Refit;
using RefitWebApiCore.Models;
using RefitWebApiCore.Models.Books;

namespace RefitWebApiCore.RestServices
{
    public interface IBookRestService : IRefitRestService
    {
        public const string BasePath = "/api";

        public const string ServiceName = BasePath + "/book";

        public const string ServiceNameWithId = BasePath + "/book/{id}";


        /// <summary>
        /// 取得書籍
        /// </summary>
        /// <param name="bookId"></param>
        /// <returns></returns>
        [Get(ServiceNameWithId)]
        Task<GetBookResult> GetAsync([AliasAs("id")] string bookId);

        /// <summary>
        /// 取得所有書籍
        /// </summary>
        /// <returns></returns>
        [Get(ServiceName)]
        Task<List<GetBookResult>> GetListAsync(PageDataQuery? query);

        /// <summary>
        /// 新增書籍
        /// </summary>
        /// <param name="book"></param>
        /// <returns></returns>
        [Post(ServiceName)]
        Task<AddBookResult> AddAsync([Body] AddBookRequest book);

        /// <summary>
        /// 更新書籍
        /// </summary>
        /// <param name="bookId"></param>
        /// <param name="book"></param>
        /// <returns></returns>
        [Put(ServiceNameWithId)]
        Task UpdateAsync([AliasAs("id")] string bookId, [Body] UpdateBookRequest book);

        /// <summary>
        /// 刪除書籍
        /// </summary>
        /// <param name="bookId"></param>
        /// <returns></returns>
        [Delete(ServiceNameWithId)]
        Task DeleteAsync([AliasAs("id")] string bookId);
    }
}
