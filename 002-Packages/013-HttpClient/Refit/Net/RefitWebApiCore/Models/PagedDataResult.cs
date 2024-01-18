namespace RefitWebApiCore.Models
{
    public class PagedDataResult<T>
    {
        public long TotalCount { get; set; } = 0;

        public List<T> Items { get; set; } = new List<T>();

        public PagedDataResult()
        {
        }
        public PagedDataResult(IEnumerable<T> items)
        {
            Items = items.ToList();
            TotalCount = items.Count();
        }
        public PagedDataResult(long totalCount, IEnumerable<T> items)
        {
            TotalCount = totalCount;
            Items = items.ToList();
        }
    }
}
