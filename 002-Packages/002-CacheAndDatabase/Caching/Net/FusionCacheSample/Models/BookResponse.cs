namespace FusionCacheSample.Models
{
    public class BookResponse
    {
        public object? Data { get; set; }


        public BookResponse()
        {

        }
        public BookResponse(object? data)
        {
            Data = data;
        }
    }
}
