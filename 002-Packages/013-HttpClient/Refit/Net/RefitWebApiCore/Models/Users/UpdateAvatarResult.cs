namespace RefitWebApiCore.Models.Users
{
    public class UpdateAvatarResult
    {
        /// <summary>
        /// 使用者 Id
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 頭像的連結
        /// </summary>
        public string AvatarUrl { get; set; } = string.Empty;
    }
}
