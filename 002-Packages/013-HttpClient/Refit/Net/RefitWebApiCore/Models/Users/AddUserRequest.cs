namespace RefitWebApiCore.Models.Users
{
    public class AddUserRequest : User
    {
        /// <summary>
        /// 使用者 ID (隱藏)
        /// </summary>
        public new Guid Id { get; set; }
    }
}
