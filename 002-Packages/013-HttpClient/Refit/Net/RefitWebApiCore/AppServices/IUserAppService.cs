using Refit;
using RefitWebApiCore.Models;
using RefitWebApiCore.Models.Users;

namespace RefitWebApiCore.AppServices
{
    public interface IUserAppService
    {
        public const string BasePath = "/api";

        public const string ServiceName = BasePath + "/user";

        public const string ServiceNameWithId = BasePath + "/user/{id}";


        /// <summary>
        /// 取得使用者
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [Get(ServiceNameWithId)]
        Task<GetUserResult> GetAsync([AliasAs("id")] string userId);

        /// <summary>
        /// 取得所有使用者
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [Get(ServiceName)]
        Task<List<GetUserResult>> GetListAsync(PageDataQuery? query);

        /// <summary>
        /// 取得登入的 Token
        /// </summary>
        /// <param name="loginInfo"></param>
        /// <returns></returns>
        [Post(ServiceName + "/token")]
        Task<UserTokenResult> GenerateTokenAsync([Body(BodySerializationMethod.UrlEncoded)] UserLoginRequest loginInfo);

        /// <summary>
        /// 新增使用者
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [Post(ServiceName)]
        Task<AddUserResult> AddAsync([Body] AddUserRequest user);

        /// <summary>
        /// 更新使用者
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        [Put(ServiceNameWithId)]
        Task UpdateAsync([AliasAs("id")] string userId, [Body] UpdateUserResuest user);

        /// <summary>
        /// 更新使用者狀態
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="activedInfo"></param>
        /// <returns></returns>
        [Patch(ServiceNameWithId + "actived")]
        Task UpdateActivedAsync([AliasAs("id")] string userId, [Body] UpdateActivedRequest activedInfo);

        /// <summary>
        /// 上傳使用者頭像
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="avatarInfo"></param>
        /// <returns></returns>
        [Multipart]
        [Post(ServiceNameWithId + "/avatar")]
        Task<UpdateAvatarResult> UploadAvatarAsync([AliasAs("id")] string userId,
                                                   [AliasAs(nameof(UpdateAvatarRequest.Avatar))] StreamPart avatar);

        /// <summary>
        /// 刪除使用者
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [Delete(ServiceNameWithId)]
        Task DeleteAsync([AliasAs("id")] string userId);
    }
}
