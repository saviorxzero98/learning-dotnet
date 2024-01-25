using Newtonsoft.Json;

namespace RefitWebApiCore.Models.Users
{
    public class User
    {
        /// <summary>
        /// 使用者 ID
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 使用者名稱
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// 密碼
        /// </summary>
        public string Password { get; set; } = string.Empty;

        /// <summary>
        /// 類型
        /// </summary>
        public int Type { get; set; } = 0;

        /// <summary>
        /// 是否啟用
        /// </summary>
        public bool IsActived { get; set; } = true;

        /// <summary>
        /// 建立日期
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// 其他屬性
        /// </summary>
        [JsonExtensionData]
        public Dictionary<string, object> Propertirs { get; set; } = new Dictionary<string, object>();
    }
}
