using System;

namespace AuditNetSample.Models
{
    public class User
    {
        public string Id { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Phome { get; set; } = string.Empty;

        public bool IsEnabled { get; set; } = true;

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
