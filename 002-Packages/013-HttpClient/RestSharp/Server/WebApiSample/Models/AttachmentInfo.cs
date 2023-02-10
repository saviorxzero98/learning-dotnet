using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace WebApiSample.Models
{
    public class AttachmentInfo
    {
        public string Id { get; set; }

        public string Type { get; set; }

        public string Name { get; set; }

        public List<IFormFile> Files { get; set; } = new List<IFormFile>();
    }
}
