using Microsoft.AspNetCore.Mvc;
using RefitWebApiCore.Extensions;
using RefitWebApiCore.Models;
using RefitWebApiCore.Models.Users;
using RefitWebApiCore.RestServices;

namespace RefitWebApiServer.Controllers
{
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRestService _appService;

        public UserController(IUserRestService appService)
        {
            _appService = appService;
        }

        [HttpGet]
        [Route(IUserRestService.ServiceNameWithId)]
        public async Task<GetUserResult> GetAsync(string id)
        {
            var result = await _appService.GetAsync(id);
            return result;
        }


        [HttpGet]
        [Route(IUserRestService.ServiceName)]
        public async Task<List<GetUserResult>> GetListAsync([FromQuery(Name = nameof(PageDataQuery.Limit))] string? limitText,
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
        [Route(IUserRestService.ServiceName + "/token")]
        public async Task<UserTokenResult> GenerateTokenAsync([FromForm] UserLoginRequest loginInfo)
        {
            var result = await _appService.GenerateTokenAsync(loginInfo);
            return result;
        }


        [HttpPost]
        [Route(IUserRestService.ServiceName)]
        public async Task<AddUserResult> AddAsync([FromBody] AddUserRequest user)
        {
            var result = await _appService.AddAsync(user);
            return result;
        }

        [HttpPut]
        [Route(IUserRestService.ServiceNameWithId)]
        public async Task UpdateAsync(string id, [FromBody] UpdateUserResuest book)
        {
            await _appService.UpdateAsync(id, book);
        }

        [HttpPatch]
        [Route(IUserRestService.ServiceNameWithId + "/actived")]
        public async Task UpdateActivedAsync(string id, [FromBody] UpdateActivedRequest activedInfo)
        {
            await _appService.UpdateActivedAsync(id, activedInfo);
        }

        [HttpPost]
        [Route(IUserRestService.ServiceNameWithId + "/avatar")]
        public async Task<UpdateAvatarResult> UploadAvatarAsync(string id, [FromForm] UpdateAvatarRequest avatarInfo)
        {
            var result = await _appService.UploadAvatarAsync(id, avatarInfo.Avatar.ToSteamPart());

            return result;
        }

        [HttpDelete]
        [Route(IUserRestService.ServiceNameWithId)]
        public async Task DeleteAsync(string id)
        {
            await _appService.DeleteAsync(id);
        }
    }
}
