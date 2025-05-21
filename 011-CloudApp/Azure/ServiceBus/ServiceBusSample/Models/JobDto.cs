using Microsoft.AspNetCore.Mvc;

namespace ServiceBusSample.Models
{
    public class JobDto
    {
        public string? Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string FunctionName {  get; set; } = string.Empty;

        public List<string> Arguments { get; set; } = new List<string>();
    }
}
