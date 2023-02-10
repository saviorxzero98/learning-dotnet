using System;

namespace WebApiSample.Models
{
    public class AccountProfile
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool IsBlocked { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
