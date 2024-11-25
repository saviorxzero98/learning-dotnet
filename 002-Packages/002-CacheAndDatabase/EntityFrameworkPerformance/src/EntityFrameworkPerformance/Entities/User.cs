using System.ComponentModel.DataAnnotations.Schema;

namespace EntityFrameworkPerformance.Entities
{
    public class User
    {
        /// <summary>
        /// User Id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// User Name
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// 是否啟用
        /// </summary>
        public bool IsActived { get; set; } = true;

        [NotMapped]
        public List<Message> UserMessages { get; set; } = new List<Message>();
    }
}
