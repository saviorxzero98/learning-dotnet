namespace RefitWebApiCore.Models.Users
{
    public class UserTokenResult
    {
        /// <summary>
        /// 使用者 Id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Token
        /// </summary>
        public string Token { get; set; } = string.Empty;

        /// <summary>
        /// Token 時效
        /// </summary>
        public DateTime ExpiredDate { get; set; }
    }
}
