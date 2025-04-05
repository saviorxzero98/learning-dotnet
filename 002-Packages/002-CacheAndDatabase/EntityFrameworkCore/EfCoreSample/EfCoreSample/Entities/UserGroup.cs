namespace EfCoreSample.Entities
{
    public class UserGroup
    {
        public string GroupId { get; set; }

        public string UserId { get; set; }

        public DateTime? CreateTime { get; set; }



        public UserGroup(string userId, string groupId)
        {
            GroupId = groupId;
            UserId = userId;
            CreateTime = DateTime.Now;
        }
    }
}
