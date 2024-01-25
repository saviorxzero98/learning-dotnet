namespace RefitWebApiCore.Models.Users
{
    public class UpdateUserResuest : User
    {
        /// <summary>
        /// 使用者 ID (隱藏)
        /// </summary>
        protected new Guid Id { get; set; }
    }
}
