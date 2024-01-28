using Microsoft.AspNetCore.Mvc;
using RefitWebApiCore.AppServices;
using RefitWebApiCore.Extensions;
using RefitWebApiCore.Models;
using RefitWebApiCore.Models.Users;
using System.Net.Mime;

namespace RefitWebApiServer.Controllers
{
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserAppService _appService;

        public UserController(IUserAppService appService)
        {
            _appService = appService;
        }

        [HttpGet]
        [Route(IUserAppService.ServiceNameWithId)]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<GetUserResult> GetAsync(string id)
        {
            var result = await _appService.GetAsync(id);
            return result;
        }


        [HttpGet]
        [Route(IUserAppService.ServiceName)]
        [Consumes(MediaTypeNames.Application.Json)]
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
        [Route(IUserAppService.ServiceName + "/token")]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<UserTokenResult> GenerateTokenAsync([FromForm] UserLoginRequest loginInfo)
        {
            var result = await _appService.GenerateTokenAsync(loginInfo);
            return result;
        }


        [HttpPost]
        [Route(IUserAppService.ServiceName)]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<AddUserResult> AddAsync([FromBody] AddUserRequest user)
        {
            var result = await _appService.AddAsync(user);
            return result;
        }

        [HttpPut]
        [Route(IUserAppService.ServiceNameWithId)]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task UpdateAsync(string id, [FromBody] UpdateUserResuest book)
        {
            await _appService.UpdateAsync(id, book);
        }

        [HttpPatch]
        [Route(IUserAppService.ServiceNameWithId + "/actived")]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task UpdateActivedAsync(string id, [FromBody] UpdateActivedRequest activedInfo)
        {
            await _appService.UpdateActivedAsync(id, activedInfo);
        }

        [HttpPost]
        [Route(IUserAppService.ServiceNameWithId + "/avatar")]
        [Consumes(MediaTypeNames.Application.Octet)]
        public async Task<UpdateAvatarResult> UploadAvatarAsync(string id, [FromForm] UpdateAvatarRequest avatarInfo)
        {
            var result = await _appService.UploadAvatarAsync(id, avatarInfo.Avatar.ToSteamPart());

            return result;
        }

        [HttpDelete]
        [Route(IUserAppService.ServiceNameWithId)]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task DeleteAsync(string id)
        {
            await _appService.DeleteAsync(id);
        }
    }
}
