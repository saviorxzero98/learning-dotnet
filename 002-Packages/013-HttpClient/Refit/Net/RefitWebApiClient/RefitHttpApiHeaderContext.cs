using System.Net.Http.Headers;

namespace RefitWebApiClient
{
    public class RefitHttpApiHeaderContext
    {
        /// <summary>
        /// Authorization
        /// </summary>
        public string Authorization { get; set; } = string.Empty;

        /// <summary>
        /// Headers
        /// </summary>
        public Dictionary<string, string> Headers { get; set; } = new Dictionary<string, string>();


        /// <summary>
        /// 設定 Authorization
        /// </summary>
        /// <param name="authorization"></param>
        /// <returns></returns>
        public RefitHttpApiHeaderContext SetAuthorization(string authorization)
        {
            Authorization = authorization;
            return this;
        }

        /// <summary>
        /// 設定其他 Headers
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public RefitHttpApiHeaderContext AddHeader(string key, string value)
        {
            if (!string.IsNullOrEmpty(key))
            {
                Headers.Add(key, value);
            }
            return this;
        }

        /// <summary>
        /// 取得 Header Value
        /// </summary>
        /// <param name="scheme"></param>
        /// <returns></returns>
        public AuthenticationHeaderValue GetAuthorizationValue(string scheme = "Bearer")
        {
            return new AuthenticationHeaderValue(scheme, Authorization);
        }
    }
}
