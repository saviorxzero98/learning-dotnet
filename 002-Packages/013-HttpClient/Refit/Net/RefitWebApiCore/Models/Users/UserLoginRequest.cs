namespace RefitWebApiCore.Models.Users
{
    public class UserLoginRequest
    {
        /// <summary>
        /// 使用者名稱
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 使用者密碼
        /// </summary>
        public string Password { get; set; }


        public UserLoginRequest()
        {

        }
        public UserLoginRequest(string name, string password)
        {
            Name = name;
            Password = password;
        }
    }
}
