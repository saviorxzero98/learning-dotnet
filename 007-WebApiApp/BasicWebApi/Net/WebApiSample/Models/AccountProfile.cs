using System;

namespace WebApiSample.Models
{
    /// <summary>
    /// 帳號資訊
    /// </summary>
    public class AccountProfile
    {
        /// <summary>
        /// 帳號 ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 帳號名稱
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// 是否被停權
        /// </summary>
        public bool IsBlocked { get; set; }

        /// <summary>
        /// 建立時間
        /// </summary>
        public DateTime CreateDate { get; set; }
    }
}
