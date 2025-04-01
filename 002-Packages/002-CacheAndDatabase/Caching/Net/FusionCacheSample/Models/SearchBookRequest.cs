namespace FusionCacheSample.Models
{
    public class SearchBookRequest
    {
        public IList<string> BookIds { get; set; } = new List<string>();

        public SearchBookRequest()
        {
        }
        public SearchBookRequest(params string[] bookIds)
        {
            BookIds = new List<string>(bookIds);
        }
    }
}
