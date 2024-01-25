using Microsoft.AspNetCore.Http;
using Refit;

namespace RefitWebApiCore.Extensions
{
    public static class IFormFileExtensions
    {
        /// <summary>
        /// 轉換成 Stream Part
        /// </summary>
        /// <param name="formFile"></param>
        /// <returns></returns>
        public static StreamPart ToSteamPart(this IFormFile formFile)
        {
            var stream = formFile.OpenReadStream();
            return new StreamPart(stream, formFile.FileName, formFile.ContentType, formFile.Name);
        }
    }
}
