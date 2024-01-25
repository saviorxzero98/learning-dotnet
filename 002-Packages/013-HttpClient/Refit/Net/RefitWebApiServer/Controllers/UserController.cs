using Microsoft.AspNetCore.Mvc;
using RefitWebApiCore.AppServices;
using RefitWebApiCore.Extensions;
using RefitWebApiCore.Models;
using RefitWebApiCore.Models.Users;
using RefitWebApiServer.Models;

namespace RefitWebApiServer.Controllers
{
    [Route(IUserAppService.BasePath)]
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
        public async Task<ActionResult> GetAsync(string id)
        {
            var result = await _appService.GetAsync(id);
            return HttpResponseResult.Ok(data: result);
        }


        [HttpGet]
        [Route(IUserAppService.ServiceName)]
        public async Task<ActionResult> GetListAsync([FromQuery(Name = nameof(PageDataQuery.Limit))] string? limitText,
                                                     [FromQuery(Name = nameof(PageDataQuery.Offset))] string? offsetText)
        {
            if (int.TryParse(limitText, out int limit) &&
                int.TryParse(offsetText, out int offset))
            {
                var query = new PageDataQuery(offset, limit);
                var results = await _appService.GetListAsync(query);
                return HttpResponseResult.Ok(data: results);
            }
            else
            {
                var results = await _appService.GetListAsync(null);
                return HttpResponseResult.Ok(data: results);
            }
        }

        [HttpPost]
        [Route(IUserAppService.ServiceName + "/token")]
        public async Task<ActionResult> GenerateTokenAsync([FromForm] UserLoginRequest loginInfo)
        {
            var result = await _appService.GenerateTokenAsync(loginInfo);
            return HttpResponseResult.Ok(data: result);
        }


        [HttpPost]
        [Route(IUserAppService.ServiceName)]
        public async Task<ActionResult> AddAsync([FromBody] AddUserRequest user)
        {
            if (user == null)
            {
                return HttpResponseResult.BadRequest();
            }

            var result = await _appService.AddAsync(user);
            return HttpResponseResult.Ok(data: result);
        }

        [HttpPut]
        [Route(IUserAppService.ServiceNameWithId)]
        public async Task<ActionResult> UpdateAsync(string id, [FromBody] UpdateUserResuest book)
        {
            if (book == null)
            {
                return HttpResponseResult.BadRequest();
            }

            await _appService.UpdateAsync(id, book);

            return HttpResponseResult.Ok();
        }

        [HttpPut]
        [Route(IUserAppService.ServiceNameWithId + "/actived")]
        public async Task<ActionResult> UpdateActivedAsync(string id, [FromBody] UpdateActivedRequest activedInfo)
        {
            if (activedInfo == null)
            {
                return HttpResponseResult.BadRequest();
            }

            await _appService.UpdateActivedAsync(id, activedInfo);

            return HttpResponseResult.Ok();
        }

        [HttpPost]
        [Route(IUserAppService.ServiceNameWithId + "/avatar")]
        public async Task<ActionResult> UploadAvatarAsync(string id, [FromForm] UpdateAvatarRequest avatarInfo)
        {
            if (avatarInfo == null || avatarInfo.Avatar == null)
            {
                return HttpResponseResult.BadRequest();
            }

            var result = await _appService.UploadAvatarAsync(id, avatarInfo.Avatar.ToSteamPart());

            return HttpResponseResult.Ok(data: result);
        }

        [HttpDelete]
        [Route(IUserAppService.ServiceNameWithId)]
        public async Task<ActionResult> DeleteAsync(string id)
        {
            await _appService.DeleteAsync(id);

            return HttpResponseResult.Ok();
        }
    }
}
