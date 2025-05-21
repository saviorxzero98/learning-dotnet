namespace ServiceBusSample.Models
{
    public class JobReportDto
    {
        public int TotalCount { get; set; } = 0;

        public int SuccessCount { get; set; } = 0;

        public int FailureCount { get; set; } = 0;
    }
}
