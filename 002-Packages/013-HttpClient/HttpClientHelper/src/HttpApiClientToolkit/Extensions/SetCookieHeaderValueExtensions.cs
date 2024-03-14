using Microsoft.Net.Http.Headers;
using System;
using System.Net;

namespace HttpApiClientToolkit.Extensions
{
    /// <summary>
    /// Set-Cookie
    /// </summary>
    public static class SetCookieHeaderValueExtensions
    {
        public static Cookie ToCookie(this SetCookieHeaderValue value)
        {
            var cookie = new Cookie()
            {
                Name = value.Name.Value,
                Value = value.Value.Value,
                Domain = value.Domain.Value,
                Secure = value.Secure,
                HttpOnly = value.HttpOnly,
                Path = value.Path.Value
            };

            if (value.Expires != null)
            {
                cookie.Expires = ((DateTimeOffset)value.Expires).LocalDateTime;
            }
            return cookie;
        }
    }
}
