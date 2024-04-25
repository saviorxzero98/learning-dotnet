using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace WebApiSample.Models
{
    /// <summary>
    /// 附件檔案
    /// </summary>
    public class AttachmentInfo
    {
        /// <summary>
        /// 附件 ID
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 檔案類型
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 檔案名稱
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 檔案內容
        /// </summary>
        public List<IFormFile> Files { get; set; } = new List<IFormFile>();
    }
}
