using Refit;
using RefitWebApiCore.AppServices;
using RefitWebApiCore.Models;
using RefitWebApiCore.Models.Users;

namespace RefitWebApiServer.AppServices
{
    public class UserAppService : IUserAppService
    {
        private readonly IServiceProvider _serviceProvider;

        public UserAppService(IServiceProvider serviceProvider) 
        {
            _serviceProvider = serviceProvider;
        }

        /// <summary>
        /// 取得使用者
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Task<GetUserResult> GetAsync(string userId)
        {
            return Task.FromResult(new GetUserResult()
            {
                Id = Guid.NewGuid(),
                Name = "Ace",
                CreateDate = DateTime.Now
            });
        }

        /// <summary>
        /// 取得所有使用者
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public Task<List<GetUserResult>> GetListAsync(PageDataQuery? query)
        {
            var results = new List<GetUserResult>()
            {
                new GetUserResult()
                {
                    Id = Guid.NewGuid(),
                    Name = "Ace",
                    CreateDate = DateTime.Now
                },
                new GetUserResult()
                {
                    Id = Guid.NewGuid(),
                    Name = "Jack",
                    CreateDate = DateTime.Now
                },
                new GetUserResult()
                {
                    Id = Guid.NewGuid(),
                    Name = "Queen",
                    CreateDate = DateTime.Now
                },
                new GetUserResult()
                {
                    Id = Guid.NewGuid(),
                    Name = "King",
                    CreateDate = DateTime.Now
                },
                new GetUserResult()
                {
                    Id = Guid.NewGuid(),
                    Name = "Joker",
                    CreateDate = DateTime.Now
                },
                new GetUserResult()
                {
                    Id = Guid.NewGuid(),
                    Name = "Alpha",
                    CreateDate = DateTime.Now
                },
                new GetUserResult()
                {
                    Id = Guid.NewGuid(),
                    Name = "Beta",
                    CreateDate = DateTime.Now
                },
                new GetUserResult()
                {
                    Id = Guid.NewGuid(),
                    Name = "Gamma",
                    CreateDate = DateTime.Now
                },
                new GetUserResult()
                {
                    Id = Guid.NewGuid(),
                    Name = "Delta",
                    CreateDate = DateTime.Now
                },
                new GetUserResult()
                {
                    Id = Guid.NewGuid(),
                    Name = "Epsilon",
                    CreateDate = DateTime.Now
                },
                new GetUserResult()
                {
                    Id = Guid.NewGuid(),
                    Name = "Zeta",
                    CreateDate = DateTime.Now
                },
                new GetUserResult()
                {
                    Id = Guid.NewGuid(),
                    Name = "Eta",
                    CreateDate = DateTime.Now
                }
            };

            if (query != null && (query.Limit > 0 || query.Offset >= 0))
            {
                results = results.Skip(query.Offset).Take(query.Limit).ToList();
            }
            return Task.FromResult(results);
        }

        /// <summary>
        /// 使用者登入
        /// </summary>
        /// <param name="loginInfo"></param>
        /// <returns></returns>
        public Task<UserTokenResult> GenerateTokenAsync(UserLoginRequest loginInfo)
        {
            return Task.FromResult(new UserTokenResult()
            {
                Id = Guid.NewGuid(),
                Token = Guid.NewGuid().ToString(),
                ExpiredDate = DateTime.Now.AddDays(1),
            });
        }

        /// <summary>
        /// 新增使用者
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Task<AddUserResult> AddAsync(AddUserRequest user)
        {
            return Task.FromResult(new AddUserResult()
            {
                Id = Guid.NewGuid()
            });
        }

        /// <summary>
        /// 更新使用者
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public Task UpdateAsync(string userId, UpdateUserResuest user)
        {
            return Task.CompletedTask;
        }

        /// <summary>
        /// 更新使用者狀態
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="activedInfo"></param>
        /// <returns></returns>
        public Task UpdateActivedAsync(string userId, UpdateActivedRequest activedInfo)
        {
            return Task.CompletedTask;
        }

        /// <summary>
        /// 上傳使用者頭像
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="avatar"></param>
        /// <returns></returns>
        public Task<UpdateAvatarResult> UploadAvatarAsync(string userId, StreamPart avatar)
        {
            return Task.FromResult(new UpdateAvatarResult()
            {
                UserId = userId,
                AvatarUrl = $"/{avatar.FileName}"
            });
        }

        /// <summary>
        /// 刪除使用者
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Task DeleteAsync(string userId)
        {
            return Task.CompletedTask;
        }
    }
}
