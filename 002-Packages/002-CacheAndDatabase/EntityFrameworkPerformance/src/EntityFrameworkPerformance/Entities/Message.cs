using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkPerformance.Entities
{
    public class Message
    {
        /// <summary>
        /// User Id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 訊息主旨
        /// </summary>
        public string Subject { get; set; } = string.Empty;

        /// <summary>
        /// 訊息內容
        /// </summary>
        public string Content { get; set; } = string.Empty;

        /// <summary>
        /// 是否刪除
        /// </summary>
        public bool IsDeleted { get; set; } = false;

        /// <summary>
        /// 訊息發送時間
        /// </summary>
        public DateTime? SendTime { get; set; }
    }
}
