namespace RefitWebApiCore.Models
{
    public class PageDataQuery
    {
        /// <summary>
        /// 第幾筆開始
        /// </summary>
        public int Offset { get; set; } = 0;

        /// <summary>
        /// 筆數
        /// </summary>
        public int Limit { get; set; } = 10;


        public PageDataQuery()
        {

        }
        public PageDataQuery(int offset, int limit)
        {
            Offset = (offset >= 0) ? offset : 0;
            Limit = limit;
        }
    }
}
