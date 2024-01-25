using Microsoft.AspNetCore.Http;

namespace RefitWebApiCore.Models.Users
{
    public class UpdateAvatarRequest
    {
        /// <summary>
        /// 檔案
        /// </summary>
        public IFormFile Avatar { get; set; }
    }
}
