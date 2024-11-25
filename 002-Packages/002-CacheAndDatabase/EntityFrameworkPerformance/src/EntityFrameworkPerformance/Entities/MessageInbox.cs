using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkPerformance.Entities
{
    public class MessageInbox
    {
        /// <summary>
        /// 訊息 ID
        /// </summary>
        public Guid MessageId { get; set; }

        /// <summary>
        /// 使用者 ID
        /// </summary>
        public Guid UserId {  get; set; }

        /// <summary>
        /// 訊息主旨
        /// </summary>
        public string Subject { get; set; } = string.Empty;

        /// <summary>
        /// 是否刪除
        /// </summary>
        public bool IsDeleted { get; set; } = false;

        /// <summary>
        /// 是否已讀
        /// </summary>
        public bool IsAlreadyRead { get; set; } = false;

        /// <summary>
        /// 訊息發送時間
        /// </summary>
        public DateTime? SendTime { get; set; }

        /// <summary>
        /// 訊息讀取時間
        /// </summary>
        public DateTime? ReadTime { get; set; }
    }
}
