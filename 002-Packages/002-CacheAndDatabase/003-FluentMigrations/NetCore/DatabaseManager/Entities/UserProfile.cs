using System;

namespace DatabaseManager.Entities
{
    public class UserProfile
    {
        public const string TableName = "UserProfile";

        public int Id { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }

        public bool IsBlocked { get; set; }

        public DateTime CreateDate { get; set; }
    }
}
