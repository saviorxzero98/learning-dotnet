namespace RefitWebApiCore.Models.Users
{
    public class GetUserResult : User
    {
        /// <summary>
        /// 密碼 (隱藏)
        /// </summary>
        public new string Password { private get; set; }
    }
}
