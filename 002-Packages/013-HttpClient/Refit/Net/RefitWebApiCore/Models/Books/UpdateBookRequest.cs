namespace RefitWebApiCore.Models.Books
{
    public class UpdateBookRequest : Book
    {
        /// <summary>
        /// 書本編號 (隱藏)
        /// </summary>
        protected new string Id { get; set; }
    }
}
