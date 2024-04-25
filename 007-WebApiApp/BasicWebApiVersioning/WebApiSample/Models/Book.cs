using System;

namespace WebApiSample.Models
{
    /// <summary>
    /// 書本
    /// </summary>
    public class Book
    {
        /// <summary>
        /// 書本編號
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 書本名稱
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// 集數
        /// </summary>
        public int Episode { get; set; }

        /// <summary>
        /// 作者
        /// </summary>
        public Author Author { get; set; }

        /// <summary>
        /// 書本價錢
        /// </summary>
        public double Price { get; set; }

        /// <summary>
        /// 發行日期
        /// </summary>
        public DateTime PublishDate { get; set; }

        /// <summary>
        /// 是否再版
        /// </summary>
        public bool IsReprint { get; set; } = false;
    }
}
