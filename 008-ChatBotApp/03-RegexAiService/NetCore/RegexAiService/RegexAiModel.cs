using System.Collections.Generic;

namespace RegexAiService
{
    public class RegexAiModel
    {
        public string Intent { get; set; } = string.Empty;
        public List<string> Patterns { get; set; } = new List<string>();

        public bool IgnoreCase { get; set; } = true;
    }
}
