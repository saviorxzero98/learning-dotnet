namespace HangfireSample.JobFilters
{
    public class SingleJobOptions
    {
        public string JobId { get; set; }

        public SingleJobOptions()
        {

        }
        public SingleJobOptions(SingleJobOptions options)
        {
            if (options != null)
            {
                JobId = options.JobId;
            }
        }
    }
}
